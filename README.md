# AspNetMvcReturnUrl
Attributes in Asp .Net MVC to handle return path and return actions

Let's say we have filtered list in "Index" view of StudentsController. Now, when the user edits one of the student and comes back to the "Index" view, then the user expects to see the filtered students again, so that he/she doesn't have to apply the filters again.

One way of handling this return is to pass the return url to "Edit" action of StudentsController, and the Edit action redirects to the return path. But, when there comes a series of navigations from action to action and then returns back to "Index" action, handling this return path becomes cumbersome. Further, if the Edit view has "Back" button, then we need to pass the return path to that Back button via ViewBag.

The AspNet.Mvc.ReturnUrl library provides two attributes, ReturnsTo and ReturnsHere, that takes care of return path itself and the developer doesn't have to worry about managing the return path url.

Note: Be sure to use distinct constants for ReturnUrlParameterName (This is the Request parameter that the library uses to formulate return url) for different ReturnsHere attributes. A ReturnsTo attribute that will return to previous ReturnsHere attribute will need to use the same constant value for ReturnUrlParameterName parameter.
