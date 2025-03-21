using System;
using System.Runtime.InteropServices;
using UnityEngine;

public class SharedMemorySender : MonoBehaviour
{
    public TxPower TxPower;
    private const string SharedMemoryName = "Omnetpp_SharedMemoryReceive";
    private const int BufferSize = 4096; 

    [DllImport("kernel32.dll", SetLastError = true)]
    private static extern IntPtr CreateFileMapping(IntPtr hFile, IntPtr lpFileMappingAttributes, uint flProtect, uint dwMaximumSizeHigh, uint dwMaximumSizeLow, string lpName);

    [DllImport("kernel32.dll", SetLastError = true)]
    private static extern IntPtr MapViewOfFile(IntPtr hMapObject, uint dwDesiredAccess, uint dwOffsetHigh, uint dwOffsetLow, uint dwNumberOfBytesToMap);

    [DllImport("kernel32.dll", SetLastError = true)]
    private static extern bool UnmapViewOfFile(IntPtr lpBaseAddress);

    [DllImport("kernel32.dll", SetLastError = true)]
    private static extern bool CloseHandle(IntPtr hObject);

    private IntPtr sharedMemoryHandle;
    private IntPtr sharedMemoryView;

    public UeManager UeManager;

    void Start()
    {
        sharedMemoryHandle = CreateFileMapping(new IntPtr(-1), IntPtr.Zero, 0x04, 0, (uint)BufferSize, SharedMemoryName);
        if (sharedMemoryHandle == IntPtr.Zero)
        {
            Debug.LogError("Failed to create shared memory.");
            return;
        }

        sharedMemoryView = MapViewOfFile(sharedMemoryHandle, 0xF001F, 0, 0, (uint)BufferSize);
        if (sharedMemoryView == IntPtr.Zero)
        {
            Debug.LogError("Failed to map view of shared memory.");
            CloseHandle(sharedMemoryHandle);
            return;
        }
    }

    private void Update()
    {
        
            SendData(UeManager.ulInfo+UeManager.dlInfo);
        
    }

    public void SendData(string data)
    {
        if (sharedMemoryView != IntPtr.Zero)
        {
            byte[] dataBytes = System.Text.Encoding.UTF8.GetBytes(data);
            if (dataBytes.Length < BufferSize)
            {
                Marshal.Copy(dataBytes, 0, sharedMemoryView, dataBytes.Length);
                Marshal.WriteByte(sharedMemoryView, dataBytes.Length, 0);
            }
            else
            {
                Debug.LogError("Data too large for shared memory.");
            }
        }
        else
        {
            Debug.LogError("Shared memory is not initialized.");
        }
    }

    void OnApplicationQuit()
    {
        if (sharedMemoryView != IntPtr.Zero)
        {
            UnmapViewOfFile(sharedMemoryView);
            sharedMemoryView = IntPtr.Zero;
        }
        if (sharedMemoryHandle != IntPtr.Zero)
        {
            CloseHandle(sharedMemoryHandle);
            sharedMemoryHandle = IntPtr.Zero;
        }
    }
}