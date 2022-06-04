using System;
using SystemSoftware.Common;

namespace SystemSoftware.MacroProcessor
{
    public static class MemoryManager
    {
        public static bool IsOverflow { get; private set; }
        public static int MemoryMaxMB { get; private set; } = 150;

        /// <summary>
        /// Вызывает сборку мусора, если был выход за границу (IsOverflow) или вызов является принужденным
        /// </summary>
        /// <param name="force"></param>
        /// <returns></returns>
        public static GCNotificationStatus CollectGarbage(bool force = false)
        {
            var status = GCNotificationStatus.Canceled;
            if (IsOverflow || force)
            {                
                GC.Collect(GC.MaxGeneration, GCCollectionMode.Forced);
                GC.WaitForPendingFinalizers();
                status = GC.WaitForFullGCComplete();
                if (status == GCNotificationStatus.Succeeded)
                    IsOverflow = false;
            }
            return status;
        }

        public static void CheckMemory(bool doFree = true)
        {
            var memoryUsageBytes = GC.GetTotalMemory(false);
            var memoryUsageMB = memoryUsageBytes / 1000 / 1000;
            if (memoryUsageMB > MemoryMaxMB)
            {
                IsOverflow = true;
                if (doFree)
                {
                    CollectGarbage();
                }
                throw new CustomException($"Обнаружена предполагаемая утечка памяти. Возможно, был запущен бесконечный цикл.");
            }
        }
    }
}
