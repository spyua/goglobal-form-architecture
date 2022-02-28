using NationalInstruments.VisaNS;
using DiconInstrument.Base.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DiconInstrument.GpibInstrument
{
    public class ComputerBoard
    {
        public Dictionary<string, GpibBoard> GPIBBoard { get; private set; }

        public ComputerBoard()
        {

        }



        public async Task<IEnumerable<string>> ScanGpibBoard()
        {

            HashSet<string> gpibBoard = new HashSet<string>();

            var task = Task.Run(() => {
                while (gpibBoard.Count==0)
                {
                    var boards = ResourceManager.GetLocalManager().FindResources("?*");

                    if (boards.Length > 0)
                    {
                        foreach (string board in boards)
                        {
                            gpibBoard.Add(board);
                        }
                    }
                }
            });

            // Timeout => 10s
            if (await Task.WhenAny(task, Task.Delay(10000)) == task)
            {
                return gpibBoard;
            }
            else
            {

                throw new TimeoutException(DeviceCode.ScanTimeOut.Description);
            }
        }


    }
}
