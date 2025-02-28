using Assets.Script.Core.GameDevelop;
using Assets.Script.Core.Server.Services;
using System;
using GameApi;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

namespace Assets.Script.Core.Server
{
    public class ServerGameLogic
    {

        public class PlayerDataLogic
        {
            public static async Task AddCharacter(Api_PlayerData playerData, string[] CharacterName)
            {
                var List = WebTool.JsonToObject<List<CharacterData_Net>>(playerData.Characters);

                for(int i = 0; i < CharacterName.Length; i++)
                {
                    var c = await AssetFInd.GetNewCharacterNet_Async(CharacterName[i]);
                    if (c == null) continue;
                    List.Add(c);
                }
                playerData.Characters = WebTool.ToJson(List);
            }
          
            public static async Task<bool> UpdataAndSave(Api_PlayerData data,string token,ulong Client_id)
            {
                if (Updata(data))
                {
                    if (!await SaveData(data, token))
                    {
                        SerReturnMsg.ReturnTokenError_BackLogin(Client_id);
                        return false;
                    }
                }

                return true ;
            }
            public static bool Updata(Api_PlayerData data)
            {
                bool Change = false; 
                DateTime now = DateTime.Now;
                #region --Stamina--
               
                if (data.LastUpDataTime == "" || data.LastUpDataTime == null)
                { 
                    Change = true;
                }
                else
                {
                    var before = WebTool.JsonToObject<DateTime>(data.LastUpDataTime);
                    var Vs = CalculateRecovery_Minutes(before, now, 3, 1);
                    if (Vs != 0) Change = true;
                    data.Stamina += Vs;
                }  
              
            
                #endregion

                if (Change)
                    data.LastUpDataTime = WebTool.ToJson(now);
                return Change;
            }

            public static bool TryChange_Stamina(Api_PlayerData data,int value)
            {
               
                if (data.Stamina < value)
                {
                    return false;
                }

                data.Stamina -= value;
                return true;
            }
            public static async Task<bool> SaveData(Api_PlayerData data,string Token)
            {
                var Sdata = new SavePlayerDataRequest();
                Sdata.api_PlayerData = data;
                Sdata.accesLogin_token = Token;
                data.Stamina = Mathf.Clamp(data.Stamina, 0, Develop_PlayerLv.GetStaminaMax(data.Lv));
                var res = await Api_Services.Get().SaveUserPlayerData(Sdata);
                return res;
            }
            public static PlayerData_Net DTO_Netdata(Api_PlayerData data) => PlayerData_Net.DTO(data);

         

            public static int CalculateRecovery_Seconds(DateTime lastUpdate, DateTime currentTime, int intervalSeconds, int recoveryPerInterval)
            {
                var elapsedSeconds = (currentTime - lastUpdate).TotalSeconds;
                return (int)(elapsedSeconds / intervalSeconds) * recoveryPerInterval;
            }
            public static int CalculateRecovery_Minutes(DateTime lastUpdate, DateTime currentTime, int intervalMinutes, int recoveryPerInterval)
            {
                var elapsedMinutes = (currentTime - lastUpdate).TotalMinutes;
                return (int)(elapsedMinutes / intervalMinutes) * recoveryPerInterval;
            }

            /// <summary>
            /// If it fails, an error message will be sent to the user.
            /// If empty data is returned, the subsequent steps will be skipped directly.
            /// </summary>
            /// <param name="token"></param>
            /// <param name="Error_Return_Client_id"></param>
            /// <returns></returns>
            public static async Task<Api_PlayerData> TokenGetPlayerData(string token, ulong Error_Return_Client_id,bool isUpdata = true)
            {
                if (token == string.Empty || token == "")
                {
                    SerReturnMsg.ReturnTokenError_BackLogin(Error_Return_Client_id);
                    return null;
                }
                  
                var res = await Api_Services.Get().GetUserPlayerData(token);
                if (!res.success)
                {
                    SerReturnMsg.ReturnTokenError_BackLogin(Error_Return_Client_id);
                    return null;
                }
                if (isUpdata)
                    Updata(res.Api_PlayerData);

                return res.Api_PlayerData;
            }


        }

    }
}
