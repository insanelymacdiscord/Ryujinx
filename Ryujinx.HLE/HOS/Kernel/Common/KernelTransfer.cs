using Ryujinx.HLE.HOS.Kernel.Process;
using ARMeilleure.Memory;

namespace Ryujinx.HLE.HOS.Kernel.Common
{
    static class KernelTransfer
    {
        public static bool UserToKernelInt32(KernelContext context, ulong address, out int value)
        {
            KProcess currentProcess = context.Scheduler.GetCurrentProcess();

            if (currentProcess.CpuMemory.IsMapped((long)address) &&
                currentProcess.CpuMemory.IsMapped((long)address + 3))
            {
                value = currentProcess.CpuMemory.ReadInt32((long)address);

                return true;
            }

            value = 0;

            return false;
        }

        public static bool UserToKernelInt32Array(KernelContext context, ulong address, int[] values)
        {
            KProcess currentProcess = context.Scheduler.GetCurrentProcess();

            for (int index = 0; index < values.Length; index++, address += 4)
            {
                if (currentProcess.CpuMemory.IsMapped((long)address) &&
                    currentProcess.CpuMemory.IsMapped((long)address + 3))
                {
                    values[index]= currentProcess.CpuMemory.ReadInt32((long)address);
                }
                else
                {
                    return false;
                }
            }

            return true;
        }

        public static bool UserToKernelString(KernelContext context, ulong address, int size, out string value)
        {
            KProcess currentProcess = context.Scheduler.GetCurrentProcess();

            if (currentProcess.CpuMemory.IsMapped((long)address) &&
                currentProcess.CpuMemory.IsMapped((long)address + size - 1))
            {
                value = MemoryHelper.ReadAsciiString(currentProcess.CpuMemory, (long)address, size);

                return true;
            }

            value = null;

            return false;
        }

        public static bool KernelToUserInt32(KernelContext context, ulong address, int value)
        {
            KProcess currentProcess = context.Scheduler.GetCurrentProcess();

            if (currentProcess.CpuMemory.IsMapped((long)address) &&
                currentProcess.CpuMemory.IsMapped((long)address + 3))
            {
                currentProcess.CpuMemory.WriteInt32((long)address, value);

                return true;
            }

            return false;
        }

        public static bool KernelToUserInt64(KernelContext context, ulong address, long value)
        {
            KProcess currentProcess = context.Scheduler.GetCurrentProcess();

            if (currentProcess.CpuMemory.IsMapped((long)address) &&
                currentProcess.CpuMemory.IsMapped((long)address + 7))
            {
                currentProcess.CpuMemory.WriteInt64((long)address, value);

                return true;
            }

            return false;
        }
    }
}