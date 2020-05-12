# Welcome on the Netfront Challenge!


Hi!

We've have a project that is currently in the development phase. The application is being built using (React.js + API) and the client want the users to be able to create blogs as well as share comments on these posts. Our mission is to build the API.

The client wants the API to be built using GraphQL in an extensible way because they have a lot of ideas for the future and think it will be easier to modify in the future.

As a team we have made some decisions about the infrastructure and the set-up of the API that cannot be changed at this time due to the delay:

-   _Pgsql & code first_
-   _Graphql-dotnet as graphql framework_
-   _Repository pattern_
-   _JWT to manage access_
-   _Public & Private graphql schema_


The specifications of the application include:

-   A user can create multiple blogs

-   A blog has multiple posts

-   A post has a list of content

Our intern started to develop the API but he is facing some issues.

Your mission, if you choose to accept it, is to firstly debug and find why our intern cannot perform some methods properly, and provide feedback on how this was solved so it can be passed on to the rest of the team.

You then have to resume where he stopped. You have to finish the "Content". The customer wants to be able to add different kind of Content in a post such as :

-   **Text**

	-   Text

-   **Image**

	-   Image url
	-   Height
	-   Width

-   **Video**
	-   Video url
	-   Duration

-   **Google** **map**
	-   Longitude
	-   Latitude

-   **Quote**

	-   Text
	-   Author

Based on our last meeting, the list will grow soon with new content types.

We won't bother implementing a proper sorting method for the current version, what the intern did is enough for now.

If you have the time to implement the delete & update method that'd be amazing.

Also if you can provide a shot concise document on "how to implement a new type" our intern will be able to do it himself in the future.

The client wants a demo of the API before Friday 5P.M, so feel free to push your changes whenever it's ready to review (in a .zip format to the address that sent you this test).

If you have any inquiry, feel free to contact me ! ✌
