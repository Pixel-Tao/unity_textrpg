using SpartaTextRPG.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpartaTextRPG.Managers
{
    public class FileManager
    {
        public static FileManager Instance { get; private set; } = new FileManager();
        private FileManager() { }

        private const string _mapCsvDir = "Resources/MapData";

        private int[][] LoadCsv(string filename)
        {
            string path = Path.Combine(_mapCsvDir, filename);

            if (File.Exists(path) == false)
                throw new Exception($"File not found: {path}");

            string text = File.ReadAllText(path);

            string[] lines = text.Split('\n', StringSplitOptions.RemoveEmptyEntries);
            int[][] data = new int[lines.Length][];
            for (int i = 0; i < lines.Length; i++)
            {
                string[] cells = lines[i].Split(',', StringSplitOptions.RemoveEmptyEntries);
                data[i] = new int[cells.Length];
                for (int j = 0; j < cells.Length; j++)
                {
                    data[i][j] = int.Parse(cells[j]);
                }
            }

            return data;
        }
        public int[][] LoadMapData(Defines.MapType mapType)
        {
            return LoadCsv($"{mapType}.csv");
        }
    }
}
