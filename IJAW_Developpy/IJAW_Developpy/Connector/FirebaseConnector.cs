using Firebase.Database;
using Firebase.Database.Query;
using System.Linq;
using System.Threading.Tasks;
using IJAW_Developpy.Models;
using System.Collections.Generic;
using System;

namespace IJAW_Developpy.Connector
{
    public class FirebaseConnector
    {

        FirebaseClient firebase = new FirebaseClient("https://xamarinfirebase-244219.firebaseio.com/");

        public async Task<List<Category>> GetAllCategories()
        {

            return (await firebase
              .Child("HeadCategories")
              .OnceAsync<Category>()).Select(item => new Category
              {
                  Name = item.Object.Name,
                  ForumId = item.Object.ForumId,
                  ThreadCount = item.Object.ThreadCount,
                  PostCount = item.Object.PostCount,
                  Description = item.Object.Description
              }).ToList();
        }

        public async Task<List<Thread>> GetAllThreads(string forumId)
        {

            var key = GetThreadDatabaseName(forumId);

            return (await firebase
              .Child(key)
              .OnceAsync<Thread>()).Select(item => new Thread
              {
                  Header = item.Object.Header,
                  ForumId = item.Object.ForumId,
                  ThreadId = item.Key,
                  CategoryName = item.Object.CategoryName,
                  DateCreated = item.Object.DateCreated,
                  DateUpdated = item.Object.DateUpdated,
                  AuthorId = item.Object.AuthorId,
                  UpdatedByAuthorId = item.Object.UpdatedByAuthorId,
                  Sticky = item.Object.Sticky,
                  PostCount = item.Object.PostCount,
                  ViewCount = item.Object.ViewCount
              }).ToList();
        }


        public async Task<List<Post>> GetAllPosts(string forumId, string threadId)
        {
            var key = GetPostDatabaseName(forumId);

            var allPosts = (await firebase
              .Child(key)
              .OnceAsync<Post>()).Select(item => new Post
              {
                  Body = item.Object.Body,
                  ThreadId = item.Object.ThreadId,
                  ThreadName = item.Object.ThreadName,
                  PostId = item.Key,
                  ForumId = item.Object.ForumId,
                  DateCreated = item.Object.DateCreated,
                  DateUpdated = item.Object.DateUpdated,
                  AuthorId = item.Object.AuthorId,
                  LikeList = item.Object.LikeList
              }).ToList();

            var returnList = new List<Post>();

            foreach (var post in allPosts)
            {
                if (post.ThreadId == threadId)
                {
                    returnList.Add(post);
                }
            }

            return returnList;
        }

        public async Task AddPost(int authorId, string forumId, string threadId, string threadName, string body)
        {

            List<int> list = new List<int>();

            var postKey = GetPostDatabaseName(forumId);

            var newPost = await firebase
              .Child(postKey)
              .PostAsync(new Post()
              {
                  Body = "",
                  ThreadId = "",
                  ThreadName = "",
                  PostId = "",
                  ForumId = 999,
                  DateCreated = "",
                  DateUpdated = "",
                  AuthorId = 0,
                  LikeList = list
              });

            await firebase
              .Child(postKey)
              .Child(newPost.Key)
              .PutAsync(new Post()
              {
                  Body = body,
                  ThreadId = threadId,
                  ThreadName = threadName,
                  PostId = newPost.Key,
                  ForumId = Convert.ToInt32(forumId),
                  DateCreated = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"),
                  DateUpdated = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"),
                  AuthorId = authorId,
                  LikeList = list
              });

        }

        public async Task<Post> AddThread(int authorId, string forumId, string header, string body)
        {
            var key = GetThreadDatabaseName(forumId);

            var newThread = await firebase
              .Child(key)
              .PostAsync(new Thread()
              {
                  Header = "",
                  ForumId = 9999,
                  ThreadId = "",
                  CategoryName = "",
                  DateCreated = "",
                  DateUpdated = "",
                  AuthorId = 0,
                  UpdatedByAuthorId = 0,
                  Sticky = false,
                  PostCount = 1,
                  ViewCount = 0
              });

            await firebase
                .Child(key)
                .Child(newThread.Key)
                .PutAsync(new Thread()
                {
                    Header = header,
                    ForumId = Convert.ToInt32(forumId),
                    ThreadId = newThread.Key,
                    CategoryName = GetCategoryName(forumId),
                    DateCreated = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"),
                    DateUpdated = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"),
                    AuthorId = authorId,
                    UpdatedByAuthorId = authorId,
                    Sticky = false,
                    PostCount = 1,
                    ViewCount = 0
                });

            List<int> list = new List<int>();

            var postKey = GetPostDatabaseName(forumId);

            var newPost = await firebase
              .Child(postKey)
              .PostAsync(new Post()
              {
                  Body = "",
                  ThreadId = "",
                  ThreadName = "",
                  PostId = "",
                  ForumId = 999,
                  DateCreated = "",
                  DateUpdated = "",
                  AuthorId = 0,
                  LikeList = list
              });

            await firebase
              .Child(postKey)
              .Child(newPost.Key)
              .PutAsync(new Post()
              {
                  Body = body,
                  ThreadId = newThread.Key,
                  ThreadName = header,
                  PostId = newPost.Key,
                  ForumId = Convert.ToInt32(forumId),
                  DateCreated = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"),
                  DateUpdated = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"),
                  AuthorId = authorId,
                  LikeList = list
              });

            return new Post()
            {
                Body = body,
                ThreadId = newThread.Key,
                ThreadName = header,
                PostId = newPost.Key,
                ForumId = Convert.ToInt32(forumId),
                DateCreated = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"),
                DateUpdated = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"),
                AuthorId = authorId,
                LikeList = list
            };

        }

        //public async Task AddPerson(int personId, string name)
        //{

        //    await firebase
        //      .Child("Users")
        //      .PostAsync(new User() { LikeCount = personId, FirstName = name});
        //}

        //public async Task<User> GetPerson(int personId)
        //{
        //    var allPersons = await GetAllPersons();
        //    await firebase
        //      .Child("Users")
        //      .OnceAsync<User>();
        //    return allPersons.Where(a => a.LikeCount == personId).FirstOrDefault();
        //}

        //public async Task UpdatePerson(int personId, string name)
        //{
        //    var toUpdatePerson = (await firebase
        //      .Child("Users")
        //      .OnceAsync<User>()).Where(a => a.Object.LikeCount == personId).FirstOrDefault();

        //    await firebase
        //      .Child("Users")
        //      .Child(toUpdatePerson.Key)
        //      .PutAsync(new User() { LikeCount = personId, FirstName = name });
        //}

        //public async Task DeletePerson(int personId)
        //{
        //    var toDeletePerson = (await firebase
        //      .Child("Users")
        //      .OnceAsync<User>()).Where(a => a.Object.LikeCount == personId).FirstOrDefault();
        //    await firebase.Child("Users").Child(toDeletePerson.Key).DeleteAsync();

        //}

        private string GetCategoryName(string forumId)
        {
            var key = "";

            switch (forumId)
            {
                case "1":
                    key = "Diskussion";
                    break;
                case "2":
                    key = "Vintage";
                    break;
                case "3":
                    key = "Kobra Kai";
                    break;
                case "4":
                    key = "Lifestyle";
                    break;
                case "5":
                    key = "Hemslöjd, modd och fix";
                    break;
                case "6":
                    key = "Resereportage";
                    break;
                case "7":
                    key = "Recensioner & Köpguider";
                    break;
            }

            return key;
        }


        private string GetPostDatabaseName(string forumId)
        {
            var key = "";

            switch (forumId)
            {
                case "1":
                    key = "DiscussionPosts";
                    break;
                case "2":
                    key = "VintagePosts";
                    break;
                case "3":
                    key = "KobraKaiPosts";
                    break;
                case "4":
                    key = "LifestylenPosts";
                    break;
                case "5":
                    key = "ModdPosts";
                    break;
                case "6":
                    key = "ReportPosts";
                    break;
                case "7":
                    key = "ReviewsPosts";
                    break;
            }

            return key;
        }
        private string GetThreadDatabaseName(string forumId)
        {
            var key = "";

            switch (forumId)
            {
                case "1":
                    key = "DiscussionThreads";
                    break;
                case "2":
                    key = "VintageThreads";
                    break;
                case "3":
                    key = "KobraKaiThreads";
                    break;
                case "4":
                    key = "LifestyleThreads";
                    break;
                case "5":
                    key = "ModdThreads";
                    break;
                case "6":
                    key = "ReportThreads";
                    break;
                case "7":
                    key = "ReviewsThreads";
                    break;
            }

            return key;
        }
    }



}