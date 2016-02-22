using Nancy;
using InventoryList;
using System.Collections.Generic;
using System;


namespace InventoryList
{
  public class HomeModule : NancyModule
  {
    public HomeModule()
    {
      Get["/"] = _ =>
      {
        return View ["index.cshtml"];
      };
      //loads index view at root//

      Post["/results"] = _ =>
      {
        string input = Request.Form["thing"];
        Thing newthing = new Thing(input);
        newthing.Save();
        List<Thing> result = Thing.GetAll();

        return View ["results.cshtml", result];
      };
    }
  }
}
