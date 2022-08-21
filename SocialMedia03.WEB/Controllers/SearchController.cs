using Microsoft.AspNetCore.Mvc;

namespace SocialMedia03.WEB.Controllers
{
    public class SearchController : Controller
    {
        [Route("hashtag/{hashtag}")]
        public IActionResult HashtagSearch(string hashtag)
        {
            ViewBag.Hashtag = hashtag;
            return View("Hashtag");
        }

        [Route("search/top")]
        public IActionResult TopSearch()
        {
            return View("Search");
        }

        [Route("search/post")]
        public IActionResult PostSearch()
        {
            return View("Search");
        }

        [Route("search/people")]
        public IActionResult PersonSearch()
        {
            return View("Search");
        }
    }
}
