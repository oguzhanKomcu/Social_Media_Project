# Social Media Project
I'm doing a social media project. In this project, I used the Unit Of Work Design Pattern, which is an enterprise design pattern, and added innovations that are different from a social media site.
-----Sql diyagram ve trigger gelicek

## DOMAIN

#### I created my Entities in my domain layer. These ;

- AppUser :  We see that my AppUser class inherits from "IdentityUser" differently. This gives us most models and properties related to users.
- FavoritePost , 
- Follower :  I have 2 AppUser foreign keys in my follow table. Thus, you can use the InverseProperty and ForeignKey attributes to configure multiple relationships between the same entities.
- Hashtag,
- Page,
- Post, 
- Post_Comment,
- Post_Score,
- PostSharing

#### I created a base interface that inherits all my entities. This is my "IBaseEntity" interface.
#### I've created my repositories' interfaces and an "IBaseRepository" base.
#### I created my "IUnitOfWork" interface, which is necessary to implement my UnitOfWork design pattern.

## INFRASTRUCTURE

- I created my infrastructure layer. In this layer, I have specified the rules to be applied while creating the tables in the database of my entities in the "Config" classes.
- I created concrete classes of my repositories. Since we are using DI here, I implemented my Crud operations in a BaseRepository concrete class and implemented it in my other concrete repository.
- While the database is being created in the Seed Data folder, I create the data that I want to be created in the table I want directly ready.
- I created my concrete class for UnitOfWork.
- First of all, when integrating the Asp.NET Core Identity library into a project, we need to evaluate the event in terms of both the library and the database. Since we use Identity in our system, we need to create the equivalent of this identity structure in the database, and we can do this with the "IdentityDbContext" class. Therefore, it is derived from the "IdentityDbContext" class in order to specify which identity the context class will work with.
## APPLİCATİON
In this layer, I created my Services and their interfaces, mapping, dto and viems, validation, extensions classes, and my container for dependency injection. I used AutoMapper for mapping, Autofac for my container, and FluentValidation for validation.

## PRESENTATION

- In this layer, I created the necessary controllers and Views for the Admin and the user.The user, who is an admin, can assign roles, post operations and user-related operations. If the admin wishes, he can use the features reserved for the user as a user. Users can comment, post, user, favorite post, shared post, follow.
