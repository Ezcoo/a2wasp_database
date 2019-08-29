using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Arma2Net.AddInProxy;
using LiteDB;

namespace A2WaspDatabase
{
    [AddIn("StorePlayerSkill")] // Name of the function
    public class StorePlayerSkill : MethodAddIn
    {
        public string Store(string args)
        {
            String[] separator = { "," };
            int count = 2;
            String[] argumentsStringArray = args.Split(separator, count, StringSplitOptions.RemoveEmptyEntries);
            int[] arguments = new int[2];

            for (int i = 0; i < arguments.Length - 1; ++i)
            {
                arguments[i] = Convert.ToInt32(argumentsStringArray[i]);
            }

            int guid = arguments[0];
            int scoreDiff = arguments[1];


            using (var db = new LiteDatabase(@"C:\Users\Administrator\Documents\Database\playerSkills.db"))
            {

                var players = db.GetCollection<Player>("players");

                if (players.Find(Query.EQ("id", guid)) == null)
                {

                    var player = new Player
                    {
                        id = guid,
                        totalScore = 0,
                        ticks = 1
                    };

                    players.Insert(player);

                    return (player.totalScore / player.ticks).ToString(); ;

                } else
                {

                    Player existingPlayer = (Player) players.FindById(guid);

                    int newScore = existingPlayer.totalScore + scoreDiff;

                    existingPlayer.totalScore = newScore;
                    existingPlayer.ticks++;

                    players.Update(existingPlayer);

                    int scorePerMin = existingPlayer.totalScore / existingPlayer.ticks;

                    return scorePerMin.ToString();

                }

            }
        }
    }
}
