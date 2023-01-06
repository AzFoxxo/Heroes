# Heroes


Heroes is a lightweight C# framework for event management and ECS solution for simple games.

I created this framework originally listening to the Sonic Heroes OST, hence the name. I've used it to create [AzzyShell](https://github.com/AzFoxxo/AzzyShell) and thought someone else may find it useful.


## Note
Please note that this project is actively in development and is not yet ready for production. The latest changes have broken the API (invocation of heroes created at game start and the changing the ECS system have resulted in breaking changes). I do not intend to maintain backwards compatibility until the project is ready for production use.

If you wish to help, please feel free to submit a pull request or open an issue. The code is not yet documented, but I will be working on that once the API is stable.

## Other projects used in this project:
- Heroes/Debug/[Paws](https://github.com/AzFoxxo/Paws) - Simple logging library by AzFoxxo
- Heroes/Graphics/[SDL2-CS](https://github.com/flibitijibibo/SDL2-CS) - SDL2 bindings for C# by flibitijibibo

## Planned Features
- [x] Event Management
- [x] Entity Component System
- [ ] Scene Management
- [WIP] Graphics (2D)
- [ ] Audio
- [ ] Input
- [ ] Networking
- [ ] Serialization