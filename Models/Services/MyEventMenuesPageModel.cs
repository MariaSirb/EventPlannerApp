using Microsoft.AspNetCore.Mvc.RazorPages;
using EventPlannerApp.Data;
namespace EventPlannerApp.Models.Services
{
    public class MyEventMenuesPageModel : PageModel
    {

        public List<AssignedMenuData> AssignedMenuDataList;
        public void PopulateAssignedMenuData(EventPlannerAppContext context,
        MyEvent myevent)
        {
            var allMenues = context.Menu;
            var x = context.MyEvent;
            var myeventMenues = new HashSet<int>(
            myevent.MyEventMenues.Select(c => c.MenuID)); 
            AssignedMenuDataList = new List<AssignedMenuData>();
            foreach (var cat in allMenues)
            {
                AssignedMenuDataList.Add(new AssignedMenuData
                {
                    MenuID = cat.ID,
                    Name = cat.ItemName,
                    Assigned = myeventMenues.Contains(cat.ID)
                });
            }
        }

       // public void PopulateAssignedMenuData(EventPlannerAppContext context,
       //string userID)
       // {

       //     var allMenues = context.Menu;
       //     var x = context.MyEvent.Where(x => x.ClientID == userID);
       //     myevent.MyEventMenues.Select(c => c.MenuID));
       //     AssignedMenuDataList = new List<AssignedMenuData>();
       //     foreach (var cat in allMenues)
       //     {
       //         AssignedMenuDataList.Add(new AssignedMenuData
       //         {
       //             MenuID = cat.ID,
       //             Name = cat.ItemName,
       //             Assigned = myeventMenues.Contains(cat.ID)
       //         });
       //     }
       // }
        public void UpdateMyEventMenues(EventPlannerAppContext context,
        string[] selectedMenues, MyEvent myeventToUpdate)
        {
            if (selectedMenues == null)
            {
                myeventToUpdate.MyEventMenues = new List<MyEventMenu>();
                return;
            }
            var selectedMenuesHS = new HashSet<string>(selectedMenues);
            var myeventMenues = new HashSet<int>
            (myeventToUpdate.MyEventMenues.Select(c => c.Menu.ID));
            foreach (var cat in context.Menu)
            {
                if (selectedMenuesHS.Contains(cat.ID.ToString()))
                {
                    if (!myeventMenues.Contains(cat.ID))
                    {
                        myeventToUpdate.MyEventMenues.Add(
                        new MyEventMenu
                        {
                            MyEventID = myeventToUpdate.ID,
                            MenuID = cat.ID
                        });
                    }
                }
                else
                {
                    if (myeventMenues.Contains(cat.ID))
                    {
                        MyEventMenu courseToRemove
                        = myeventToUpdate
                        .MyEventMenues
                        .SingleOrDefault(i => i.MenuID == cat.ID);
                        context.Remove(courseToRemove);
                    }
                }
            }
        }


    }
}
