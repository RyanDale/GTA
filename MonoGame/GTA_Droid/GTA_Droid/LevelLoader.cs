using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Microsoft.Xna.Framework;


namespace GTA_Droid
{
    class LevelLoader
    {
        public LevelLoader(String levelName)
        {
            LoadLevel();

        }
        public void LoadLevel()
        {
            try
            {
                
                
                StreamReader sr = new StreamReader(TitleContainer.OpenStream("Level_p.txt"));
                List<String> level = new List<String>();
                String line;
                int i = 0, j = 0;
                while ((line = sr.ReadLine()) != null)
                {
                    String[] s = line.Split();
                    if (s[0] != "null")
                        new Tile(new Vector2(i * 512, j * 512), s[0], MathHelper.ToRadians(float.Parse(s[1])));
                    i++;
                    if (i >= 10)
                    {
                        i = 0;
                        j++;
                        sr.ReadLine();

                    }
                }
                sr.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("new Exception "+e);
            }

        }
    }
}
