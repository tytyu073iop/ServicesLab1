using Xunit.Sdk;

namespace TestBackend;

public class DBManagerTest
{

    [Fact]
    public void TestGettingAll()
    {

        DBManager dbm = new(PathHelper.GetFilesDirectory("test.sqlite3"));
        List<Roster> arr = [
            new Roster("adamlem", 12, "Mike", "Adamle", "RW", "2001-09-21 00:00:00", 73, 197, "Stamford", "CT"),
            new Roster("adamles", 17, "Scott", "Adamle", "D", "1999-03-01 00:00:00", 70, 184, "Columbus", "OH")
            ];

        List<Roster> result = dbm.GetAllRosters();

        Assert.Equal(result, arr);
    }

    private static void CreateRosterTable(DBManager dbm)
    {
        var command = dbm.connection.CreateCommand();
        command.CommandText = """
        CREATE TABLE roster (
    playerid VARCHAR(10) PRIMARY KEY,
    jersey INTEGER NOT NULL,
    fname VARCHAR(50) NOT NULL,
    sname VARCHAR(50) NOT NULL,
    position VARCHAR(5) NOT NULL,
    birthday DATETIME NOT NULL,
    weight INTEGER NOT NULL,
    height INTEGER NOT NULL,
    birthcity VARCHAR(50) NOT NULL,
    birthstate VARCHAR(5) NOT NULL
)
""";

        command.ExecuteNonQuery();
    }

    [Fact]
    public void TestAddition()
    {
        DBManager dbm = new(":memory:");
        CreateRosterTable(dbm);
        Roster test = new("adamlem", 12, "Mike", "Adamle", "RW", "2001-09-21 00:00:00", 90, 197, "Stamford", "CT");

        dbm.AddRoster(test);

        Assert.Equal([test], dbm.GetAllRosters());
    }
}
