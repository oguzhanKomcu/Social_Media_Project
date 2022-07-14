# Social Media Project
I'm doing a social media project. In this project, I used the Unit Of Work Design Pattern, which is an enterprise design pattern, and added innovations that are different from a social media site.

## DOMAIN
#### I created my Entities in my domain layer. These ;
- AppUser :  We see that my AppUser class inherits from "IdentityUser" differently. This gives us most models and properties related to users.
- FavoritePost , 
- Follower, 
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
- I have 2 AppUser foreign keys in my follow table. Thus, you can use the InverseProperty and ForeignKey attributes to configure multiple relationships between the same entities.
