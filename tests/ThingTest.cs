using Xunit;
using System.Collections.Generic;
using System;
using System.Data;
using System.Data.SqlClient;

namespace InventoryList
{
  public class InventoryTest : IDisposable
  {
    public InventoryTest()
    {
      DBConfiguration.ConnectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=Inventory_test;Integrated Security=SSPI;";
    }

    public void Dispose()
    {
      Thing.DeleteAll();
    }

    [Fact]
    public void Test_DatabaseEmptyAtFirst()
    {
      //Arrange, Act
      int result = Thing.GetAll().Count;

      //Assert
      Assert.Equal(0, result);
    }

    [Fact]
    public void Test_Equal_ReturnsTrueIfDescriptionsAreTheSame()
    {
  //Arrange, Act
    Thing firstThing = new Thing("Mow the lawn");
    Thing secondThing = new Thing("Mow the lawn");

  //Assert
    Assert.Equal(firstThing, secondThing);
    }
    [Fact]

    public void Test_Save_SavesToDatabase()
    {
      //Arrange
    Thing testThing = new Thing("Mow the lawn");

    //Act
    testThing.Save();
    List<Thing> result = Thing.GetAll();
    List<Thing> testList = new List<Thing>{testThing};

    //Assert
    Assert.Equal(testList, result);
    }

    [Fact]
    public void Test_Save_AssignsIdToObject()
    {
      //Arrange
      Thing testThing = new Thing("Mow the lawn");

      //Act
      testThing.Save();
      Thing savedThing = Thing.GetAll()[0];

      int result = savedThing.GetId();
      int testId = testThing.GetId();

      //Assert
      Assert.Equal(testId, result);
    }
    [Fact]
    public void Test_Find_FindsThingInDatabase()
    {
      //Arrange
      Thing testThing = new Thing("Mow the lawn");
      testThing.Save();

      //Act
      Thing foundThing = Thing.Find(testThing.GetId());

      //Assert
      Assert.Equal(testThing, foundThing);
    }
  }
}
