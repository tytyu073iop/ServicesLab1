using Microsoft.Data.Sqlite;
using ServiceReference1;

public class DBManager
{
    public ServiceReference1.DBDriverToRosterClient dbdriver = new();


    public Task<ServiceReference1.Roster[]> GetAllRosters()
    {

        return dbdriver.GetAllRostersAsync();
    }

        private static void CheckRosterWithThrowing(Roster player)
        {

            // Rule 2: If position is Defender and height > 185, weight must be >= 80
            if (player.Position == "RW" && player.Height > 185 && player.Weight < 80)
            {
                throw new Exception($"Rule 2 violated: {player.Fname} {player.Sname} is underweight for a tall defender.");
            }

            // Rule 4: If name starts with 'А', position is Forward, height < 175, weight must be < 70
            if (player.Fname.StartsWith("А") && player.Position == "LW" && player.Height < 175 && player.Weight >= 70)
            {
                throw new Exception($"Rule 4 violated: {player.Fname} {player.Sname} is too heavy for a short forward.");
            }
        }

        public async Task AddRoster(Roster roster)
        {
            CheckRosterWithThrowing(roster);

            await dbdriver.AddRosterAsync(roster);

        }

        public DBManager()
        {

        }

        ~DBManager()
        {

        }
    }
