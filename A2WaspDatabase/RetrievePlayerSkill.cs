using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Arma2Net.AddInProxy;
using LiteDB;

namespace A2WaspDatabase
{
    [AddIn("RetrievePlayerSkill")] // Name of the function
    public class RetrievePlayerSkill : MethodAddIn
    {
        public string Retrieve(string args)
        {
            String[] separator = { "," };
            int count = 1;
            String[] argumentsStringArray = args.Split(separator, count, StringSplitOptions.RemoveEmptyEntries);
            int[] arguments = new int[2];

            for (int i = 0; i < arguments.Length - 1; ++i)
            {
                arguments[i] = Convert.ToInt32(argumentsStringArray[i]);
            }

            int guid = arguments[0];
            using (var db = new LiteDatabase(@"C:\DB\playerSkills.db"))
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

                    return (player.totalScore / player.ticks).ToString();
                }
                else
                {
                    Player existingPlayer = (Player) players.FindById(guid);

                    return (existingPlayer.totalScore / existingPlayer.ticks).ToString();
                }

            }
        }
    }

}