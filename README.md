# Social Media Project
I'm doing a social media project. In this project, I used the Unit Of Work Design Pattern, which is an enterprise design pattern, and added innovations that are different from a social media site.

# DOMAIN
#### I created my Entities in my domain layer. These ;
- AppUser,
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
