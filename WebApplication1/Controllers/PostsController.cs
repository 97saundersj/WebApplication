using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;

namespace WebApplication1.Models
{
    [Authorize]
    public class PostsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PostsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Posts
        public async Task<IActionResult> Index(string sortOrder, string searchString)
        {
            List<Post> posts;

            if (!String.IsNullOrEmpty(searchString))
            {
                posts = await _context.Posts.Where(x => x.Title.Contains(searchString)).ToListAsync();
            }
            else
            {
                posts = await _context.Posts.ToListAsync();
            }

            switch (sortOrder)
            {
                case "Date":
                    posts = posts.OrderBy(x => x.Published).ToList();
                    break;
                case "Date-Descending":
                    posts = posts.OrderByDescending(x => x.Published).ToList();
                    break;
                default:
                    posts = posts.OrderBy(x => x.Published).ToList();
                    break;
            }

            return View(posts);
        }

        // GET: Posts/Comments/5
        public async Task<IActionResult> Comments(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var post = await _context.Posts
                .FirstOrDefaultAsync(m => m.ID == id);
            if (post == null)
            {
                return NotFound();
            }

            PostCommentsViewModel viewModel = await GetPostDetailsViewModelFromPost(post);

            return View(viewModel);
        }

        // Post: Posts/Comments/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Comments([Bind("PostID,CommentContent")]
            PostCommentsViewModel viewModel)
        {
            Post post = await _context.Posts
                .SingleOrDefaultAsync(m => m.ID == viewModel.PostID);

            if (post == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                Comment comment = new Comment();


                comment.Content = viewModel.CommentContent;
                comment.Username = User.Identity.Name;
                comment.Published = DateTime.Now;

                comment.MyPost = post;

                _context.Comments.Add(comment);
                await _context.SaveChangesAsync();

                viewModel = await GetPostDetailsViewModelFromPost(post);
            }
            else
            {
                viewModel = await GetPostDetailsViewModelFromPost(post);
            }

            return View(viewModel);
        }

        private async Task<PostCommentsViewModel> GetPostDetailsViewModelFromPost(Post post)
        {
            PostCommentsViewModel viewModel = new PostCommentsViewModel();

            viewModel.Post = post;
            viewModel.Content = post.Content;

            //Get comments from database
            List<Comment> comments = await _context.Comments
               .Where(x => x.MyPost == post).ToListAsync();

            viewModel.Comments = comments;
            return viewModel;
        }


        // GET: Posts/Create
        [Authorize(Policy = "CanCreatePostsClaim")]
        public IActionResult Create()
        {
            ViewBag.Message = "Hello There";
            return View();
        }

        // POST: Posts/Create
        [HttpPost]
        [Authorize(Policy = "CanCreatePostsClaim")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Title,Content")] Post post)
        {
            if (ModelState.IsValid)
            {
                post.Username = User.Identity.Name;
                post.Published = DateTime.Now;

                _context.Add(post);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));

            }
            return View(post);
        }

        // GET: Posts/Edit/5
        [Authorize(Policy = "CanCreatePostsClaim")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var post = await _context.Posts.FindAsync(id);
            if (post == null)
            {
                return NotFound();
            }
            return View(post);
        }

        // POST: Posts/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "CanCreatePostsClaim")]
        public async Task<IActionResult> Edit(int? id, [Bind("ID,Title,Content")] Post post)
        {
            if (id != post.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                post.Username = User.Identity.Name;
                post.Published = DateTime.Now;

                try
                {
                    _context.Update(post);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PostExists(post.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Comments", new { id });
            }
            return View(post);
        }
   
        // GET: Posts/Delete/5
        [Authorize(Policy = "CanDeletePostsClaim")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var post = await _context.Posts
                .FirstOrDefaultAsync(m => m.ID == id);
            if (post == null)
            {
                return NotFound();
            }

            return View(post);
        }

        // POST: Posts/Delete/5
        [Authorize(Policy = "CanDeletePostsClaim")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var post = await _context.Posts.FindAsync(id);
            _context.Posts.Remove(post);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PostExists(int id)
        {
            return _context.Posts.Any(e => e.ID == id);
        }
    }

}
