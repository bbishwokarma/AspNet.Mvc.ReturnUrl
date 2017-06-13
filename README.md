# AspNetMvcRedirectAssist
### Attributes in Asp .Net MVC to handle filter parameters applied in an action in which we will be returning back later

Let's say we have filtered list in "Index" view of StudentsController. Now, when the user edits one of the student and comes back to the "Index" view, then the user expects to see the filtered students again, so that he/she doesn't have to apply the filters again.

One way of handling this return is to pass the return url to "Edit" action of StudentsController, and the Edit action redirects to the return path. But, when there comes a series of navigations from action to action and then returns back to "Index" action, handling this return path becomes cumbersome. Further, if the Edit view has "Back" button, then we need to pass the return path to that Back button via ViewBag.

The AspNet.Mvc.AspNetMvcRedirectAssist library provides two attributes, RedirectsBackUsingParameter and ReturnsUsingParameter, that takes care of return path itself and the developer doesn't have to worry about managing the return path url.

### Note: 
Be sure to use distinct constants for ReturnUrlParameterName (This is the Request parameter that the library uses to formulate return url) for different ReturnsUsingParameter attributes. A RedirectsBackUsingParameter attribute that will return to previous ReturnsUsingParameter attribute will need to use the same constant value for ReturnUrlParameterName parameter.

## Example
Let we have model University and controller UniversitiesController. The Index action shows the list of universities after applying the filter and order.

```CS
public class UniversitiesController : Controller
{
	//GET
	[ReturnsUsingParameter("RP_universities")]
	public ActionResult Index(UniversitySearchParams searchParams, UniversityOrderbyParams orderBy){
	   ...
	   return View(list);
	}
	
	//GET
	[RedirectsBackUsingParameter("RP_universities")]
	public ActionResult Edit(string id)
	{
		...
		//this does not returns to RP_universities
		return View(university);
	}

	[HttpPost]
	public ActionResult Edit(University university)
	{
		if (ModelState.IsValid)
		{
			...
			//this returns to RP_universities
			return RedirectToAction("Index");
		}
		...
		//this does not returns to RP_universities
		return View(university);
	}

}

```

Further, we have StudentsController for Student model. Index of StudentsController takes universityId to show the list of students in that university. This action is referenced from UniversitiesController.Index view.


```CS
public class StudentsController : Controller
{
	//GET
	[RedirectsBackUsingParameter("RP_universities")]
	[ReturnsUsingParameter("RP_students")]
	public ActionResult Index(String universityId, StudentSearchParams searchParams, StudentOrderbyParams orderBy){
	   ...
	   if(...){
			//this returns to RP_universities
			//No need to worry about filter parameters applied in UniversitiesController.Index
			return RedirectToAction("Index", "Universities");
	   }
	   ...
	   return View(list);
	}
	
	//GET
	[RedirectsBackUsingParameter("RP_students")]
	public ActionResult Edit(string id)
	{
		...
		//this does not returns to RP_students
		return View(university);
	}

	[HttpPost]
	public ActionResult Edit(University university)
	{
		if (ModelState.IsValid)
		{
			...
			//this returns to RP_students
			return RedirectToAction("Index");
		}
		...
		//this does not returns to RP_students
		return View(university);
	}

}

```

Further, StudentsController.Index view can have a url back to UniversitiesController.Index action as:

```CS
//No need to worry about filter parameters applied in UniversitiesController.Index
@Html.ActionLink("Back to Universities", "Index", "Universities") 
```
##### [Bikash Bishwokarma](https://bbishwokarma.github.io/)

