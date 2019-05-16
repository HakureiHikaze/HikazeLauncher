using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace libMcArgs
{
    /// <summary>
    /// 参数生成器
    /// </summary>
    public class ParamentsGen
    {
        /// <summary>
        /// 连接游戏JVM和mc参数
        /// </summary>
        /// <param name="player">玩家信息,Playerinfo类</param>
        /// <param name="JavaPath">Java路径</param>
        /// <param name="GameRoot">游戏根路径(.minecraft的所在目录)</param>
        /// <param name="GameVersion">游戏版本，存储version内的文件夹全称的string</param>
        /// <param name="ExtraParaments">额外的JVM参数</param>
        /// <param name="MaxMem">设置最大内存，单位为M的整型</param>
        /// <param name="MinMem">设置最小内存，单位为M的整型</param>
        /// <param name="isOldMc">判断是否为旧版启动的Minecraft</param>
        /// <returns>返回连接好的游戏参数</returns>
        public string CombineParaments(
            Playerinfo player,
            string JavaPath,
            string GameRoot,
            string GameVersion,
            string ExtraParaments,
            int MaxMem,
            int MinMem,
            bool isOldMc
            )
        {
 /*
 * -XX:-UseAdaptiveSizePolicy
 * -XX:-OmitStackTraceInFastThrow
 * -Dfml.ignoreInvalidMinecraftCertificates=true
 * -Dfml.ignorePatchDiscrepancies=true
 * -XX:HeapDumpPath=MojangTricksIntelDriversForPerformance_javaw.exe_minecraft.exe.heapdump
 * \"-Dminecraft.launcher.brand=Hello Minecraft! Launcher\"
 * -Dminecraft.launcher.version=2.7.9.55";
*/
            string argOut;
            System.IO.StreamReader file = System.IO.File.OpenText(GameRoot + "\\.minecraft\\versions\\" + GameVersion + "\\" + GameVersion + ".json");
            JsonTextReader reader = new JsonTextReader(file);
            JObject VersionJson = (JObject)JToken.ReadFrom(reader);
            string MemPara = "-Xmn" + Convert.ToString(MinMem) + "M "+"-Xmx" + Convert.ToString(MaxMem) + "M";
            string Extra = "-Djava.library.path=" + GameRoot + "\\.minecraft\\versions\\" + GameVersion + "\\" + GameVersion + "-natives";
            string _cp = "-cp \""+GetPackParameters(GameRoot,GameVersion)+GameRoot+"\\.minecraft\\versions\\"+GameVersion+"\\"+GameVersion+".jar\"";
            string MinecraftParamentsRaw=VersionJson["minecraftArguments"].ToString();
            string GameBaseVersion=VersionJson["inheritsFrom"].ToString();
            string MainClass = VersionJson["mainClass"].ToString();
            MinecraftParamentsRaw = MinecraftParamentsRaw.Replace("${auth_player_name}", player.PlayerName);
            MinecraftParamentsRaw = MinecraftParamentsRaw.Replace("${version_name}", GameVersion);
            MinecraftParamentsRaw = MinecraftParamentsRaw.Replace("${game_directory}", GameRoot+"\\.minecraft");
            MinecraftParamentsRaw = MinecraftParamentsRaw.Replace("${assets_root}", GameRoot + "\\.minecraft\\assets");
            MinecraftParamentsRaw = MinecraftParamentsRaw.Replace("${assets_index_name}", GameBaseVersion);
            MinecraftParamentsRaw = MinecraftParamentsRaw.Replace("${auth_uuid}", player.uuid);
            MinecraftParamentsRaw = MinecraftParamentsRaw.Replace("${auth_access_token}", player.uuid);
            MinecraftParamentsRaw = MinecraftParamentsRaw.Replace("${user_type}", "Legacy");
            MinecraftParamentsRaw = MinecraftParamentsRaw.Replace("${version_type}", "HikazeLauncher\\OVO/");
            argOut =                              " " +
                        MemPara                 + " " +//内存项
                        Extra                   + " " +//
                        ExtraParaments          + " " +
                        _cp                     + " " +
                        MainClass               + " " +
                        MinecraftParamentsRaw   + " ";
            return argOut;
        }
        /// <summary>
        /// 用包名获取运行库路径
        /// </summary>
        /// <param name="PackName">包名</param>
        /// <returns></returns>
        private string ExportJarPack(string PackName)
        {
            string re ="";
            bool Colon = false;
            for (int i =0;i<PackName.Length;i++)
            {
                if (PackName[i] == '.')
                {
                    if(Colon)
                    {
                        if((('0'<=PackName[i-1] && PackName[i - 1]<= '9') && ('0' <= PackName[i + 1] && PackName[i + 1] <= '9'))
                            || (('0' <= PackName[i - 1] && PackName[i - 1] <= '9') && (PackName[i+1] == 'F')))//这是个特殊情况
                        {
                            re += ".";
                        }
                        else
                        {
                            re += "\\";
                        }
                    }
                    else
                    {
                        re += "\\";
                    }
                }
                else if (PackName[i] == ':')
                {
                    Colon = true;
                    re += "\\";
                    continue;
                }
                else
                {
                    re += PackName[i];
                }
            }
            return re;
        }
        /// <summary>
        /// 用包名获取运行库文件名
        /// </summary>
        /// <param name="PackName">包名</param>
        /// <returns></returns>
        private string GetJarPackName(string PackName)
        {
            string re = "";
            bool Colon = false;
            for (int i=0;i<PackName.Length;i++)
            {
                if(PackName[i]==':')
                {
                    if(Colon)
                    {
                        re += "-";
                        continue;
                    }
                    Colon = true;
                }
                else if(Colon)
                {
                    re += PackName[i];
                }
            }
            return re+".jar";
        }
        /// <summary>
        /// 从版本Json文件获取包名列表
        /// </summary>
        /// <param name="GameRoot">游戏根目录，即.minecraft文件夹所在目录</param>
        /// <param name="Version">目标版本</param>
        /// <param name="PackList">包名列表</param>
        /// <returns>返回包名列表</returns>
        private ArrayList GetPacksFromJson(string GameRoot,string Version, ArrayList PackList)
        {
            System.IO.StreamReader JsonFile = System.IO.File.OpenText(GameRoot +"\\.minecraft\\versions\\"+Version+"\\"+Version+".json");
            JsonTextReader reader = new JsonTextReader(JsonFile);
            JObject JReading = (JObject)JToken.ReadFrom(reader);
            if(JReading["inheritsFrom"] != null)
            {
                PackList.AddRange(GetPacksFromJson(GameRoot, JReading["inheritsFrom"].ToString(), PackList));
            }
            foreach(JToken elements in JReading["libraries"])
            {
                PackList.Add(elements["name"].ToString());
            }
            return PackList;
        }
        /// <summary>
        /// 从列表转换包名为包路径
        /// </summary>
        /// <param name="PackList">包的列表</param>
        /// <returns>返回包的列表</returns>
        private ArrayList ConvertPackNameFromList(ArrayList PackList)
        {
            for(int i=0;i<PackList.Count;i++)
            {
                PackList[i] = ExportJarPack((string)PackList[i])+"\\" + GetJarPackName((string)PackList[i]);
            }
            return PackList;
        }
        /// <summary>
        /// 移除列表中重复项
        /// </summary>
        /// <param name="List">列表</param>
        /// <returns>返回新列表</returns>
        private ArrayList RemoveDuplicate(ArrayList List)
        {
            for (int i = 0; i < List.Count; i++)
            {
                for (int j = i + 1; j < List.Count; j++)
                {
                    if (List[i].ToString() ==List[j].ToString())
                    {
                        List.RemoveAt(j);
                        j--;
                    }
                }
            }
            return List;
        }
        private string GetPackParameters(string GameRoot,string GameVersion)
        {
            string ReturnString = "";
            ArrayList List = new ArrayList();
            List = GetPacksFromJson(GameRoot, GameVersion, List);
            List = ConvertPackNameFromList(List);
            List = RemoveDuplicate(List);
            for (int i =0;i<List.Count;i++)
            {
                ReturnString +=GameRoot+"\\.minecraft\\libraries\\"+List[i].ToString() + ";";
            }
            return ReturnString+";";
        }
    }
}
