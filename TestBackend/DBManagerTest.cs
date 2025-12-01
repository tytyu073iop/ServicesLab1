using Xunit.Sdk;
using ServiceReference1;
using System.Threading.Tasks;

namespace TestBackend;

// WARNING: works only when dbdriver is on
public class DBManagerTest
{

    [Fact]
    public async Task TestGettingAll()
    {

        DBManager dbm = new();

        Roster[] result = await dbm.GetAllRosters();

        Assert.Equal([], result);
    }



    [Fact]
    public async Task TestAddition()
    {
        DBManager dbm = new();
        Roster test = new()
        {
            Playerid = "adamlem",
            Jersey = 12,
            Fname = "Mike",
            Sname = "Adamle",
            Position = "RW",
            Birthday = "2001-09-21 00:00:00",
            Height = 90,
            Weight = 197,
            Birthcity = "Stamford",
            Birthstate = "CT"
        };

        await dbm.AddRoster(test);

        Assert.Equal([test], await dbm.GetAllRosters());
    }

    [Fact]
    public async Task TestAdditionFalure()
    {
        DBManager dbm = new();
        Roster test = new() { Playerid = "adamlem", Jersey = 12, Fname = "Mike", Sname = "Adamle", Position = "RW", Birthday = "2001-09-21 00:00:00", Height = 197, Weight = 70, Birthcity = "Stamford", Birthstate = "CT" };

        await Assert.ThrowsAsync<Exception>(async () => await dbm.AddRoster(test));
    }
}
