# Ef Bloggy Improved

It was not a direct assignment to perform, but we would do it more ourselves or in a group. The goal was that we would gain some knowledge entity framework (EF). In my opinion, this is an improved version of the basic code that we started working on " EfBloggy ". It is certainly deliberately done that these possibilities do not exist from the beginning, but you must realize this yourself a little later when we carry out a project where we can use previous tasks as a basis. May see what I will choose a little later in the course when it comes time to complete this project.

We got access to a console application to handle various blog posts with a default database. If you just read the instructions for the application, everything will go well… Have therefore added some code that checks if the database exists and if not, the user is prompted to run the Update-Database in the package manger console.

I was also a little annoyed that there was no separate table / class to handle the different writers for posts. Prefers to normalize data as far as possible! We had as an exercise task to add the possibility to add a new blog post or delete blog posts in the service. Also added a timestamp on the posts to make it a little more lifelike. Therefore, I developed this improved version of the code with one to many relationships in the database.
