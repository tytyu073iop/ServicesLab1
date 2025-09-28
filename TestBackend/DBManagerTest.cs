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



    [Fact]
    public void TestAddition()
    {
        DBManager dbm = new(":memory:");
        DBPreparation.PrepareInMemoryDb(dbm);
        Roster test = new("adamlem", 12, "Mike", "Adamle", "RW", "2001-09-21 00:00:00", 90, 197, "Stamford", "CT");

        dbm.AddRoster(test);

        Assert.Equal([test], dbm.GetAllRosters());
    }

    [Fact]
    public void TestAdditionFalure()
    {
        DBManager dbm = new(":memory:");
        DBPreparation.PrepareInMemoryDb(dbm);
        Roster test = new("adamlem", 12, "Mike", "Adamle", "RW", "2001-09-21 00:00:00", 70, 197, "Stamford", "CT");

        Assert.Throws<Exception>(() => dbm.AddRoster(test));
    }
}
