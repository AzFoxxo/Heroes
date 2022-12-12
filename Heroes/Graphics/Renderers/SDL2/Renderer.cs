using Heroes.Graphics.Renderers.SDL2.Bindings;
using Heroes.Debug.Paws;

namespace Heroes.Graphics.Renderers.SDL2;

/// <summary> Sprite </summary>
public struct Sprite
{
    public IntPtr Texture { get; set; }
    public int X { get; set; }
    public int Y { get; set; }
    public int SizeX { get; set; }
    public int SizeY { get; set; }

    public Sprite(IntPtr texture, int x, int y, int sizeX, int sizeY)
    {
        Texture = texture;
        X = x;
        Y = y;
        SizeX = sizeX;
        SizeY = sizeY;
    }
}

public class Renderer
{
    IntPtr window;
    IntPtr renderer;

    private List<Sprite> sprites = new();

    static public Renderer Instance;

    private bool cleanedUp = false;

    public Renderer()
    {
        Instance = this;
    }

    ~Renderer() {
        if (!cleanedUp) CleanUp();
    }

    /// <summary> Add a sprite to the renderer. </summary>
    /// <param name="sprite"> The sprite to add. </param>
    public void AddSprite(Sprite sprite)
    {
        sprites.Add(sprite);
    }

    /// <summary> Remove a sprite from the renderer. </summary>
    /// <param name="sprite"> The sprite to remove. </param>
    public void RemoveSprite(Sprite sprite)
    {
        sprites.Remove(sprite);
    }

    /// <summary> Update a sprite in the renderer. </summary>
    /// <param name="sprite"> The sprite to update. </param>
    public void UpdateSprite(Sprite sprite)
    {
        // Find the exact sprite in the list
        var index = sprites.FindIndex(s => s.Texture == sprite.Texture);

        // Check if the sprite was found
        if (index == -1)
        {
            PawsLogger.Error("The sprite was not found in the list.");
            return;
        }

        // Update the sprite
        sprites[index] = sprite;

    }

    // List of textures that have been loaded
    Dictionary<string, IntPtr> textures = new();

    /// <summary> Load an image into memory and return a pointer to it. </summary>
    /// <param name="path"> The path to the image. </param>
    /// <returns> A pointer to the image. </returns>
    public IntPtr LoadImage(string path)
    {
        // If an item with the key path exists, return it
        if (textures.ContainsKey(path))
        {
            PawsLogger.Debug($"Texture {path} already exists in memory.");
            return textures[path];
        }

        // Load the image into memory
        var image = SDL_image.IMG_Load(path);

        // Check if the image was loaded successfully
        if (image == IntPtr.Zero)
        {
            PawsLogger.Error($"There was an issue loading the image. {SDL.SDL_GetError()}");
        }

        // Create a new texture from the image
        var texture = SDL.SDL_CreateTextureFromSurface(renderer, image);

        // Check if the texture was created successfully
        if (texture == IntPtr.Zero)
        {
            PawsLogger.Error($"There was an issue creating the texture. {SDL.SDL_GetError()}");
        }

        // Free the image from memory
        SDL.SDL_FreeSurface(image);

        // Add to the list of textures
        textures.Add(path, texture);

        // Log the loaded texture
        PawsLogger.Info($"Loaded the file {path} in memory and created a texture.");

        // Return the reference to the texture in memory
        return texture;
    }

    /// <summary> Draw a texture to the screen. </summary>
    /// <param name="texture"> The texture to draw. </param>
    /// <param name="x"> The X position to draw the texture at. </param>
    /// <param name="y"> The Y position to draw the texture at. </param>
    public void DrawTexture(IntPtr texture, int x, int y, int sizeX, int sizeY)
    {
        // Create a new rectangle to draw the texture to
        var rect = new SDL.SDL_Rect
        {
            x = x,
            y = y,
            w = sizeX,
            h = sizeY
        };

        // Draw the texture to the screen
        SDL.SDL_RenderCopy(renderer, texture, IntPtr.Zero, ref rect);
    }

    public void Entry()
    {
        // Setup all of the SDL resources
        Setup();

        // Unfreeze the game loop
        Heroes.Internal.GameLoop.FreezeExecuting = false;

        // // Load an image into memory
        var texture = LoadImage("Examples/Sprites/Player.png");

        // // Add a sprite to the renderer
        AddSprite(new Sprite(texture, 0, 0, 32, 32));

        // While the game loop is running, poll for events and render.
        while (Heroes.Internal.GameLoop.Running)
        {
            PollEvents();
            Render();
        }

        // Clean up all of the SDL resources
        CleanUp();
    }

    /// <summary>
    /// Setup all the SDL resources
    /// </summary>
    void Setup()
    {
        // Initialise SDL with everything
        if (SDL.SDL_Init(SDL.SDL_INIT_EVERYTHING) < 0)
        {
            PawsLogger.Error($"There was an issue initialising SDL. {SDL.SDL_GetError()}");
        } else PawsLogger.Info("SDL has been initialised without issues.");

        // Create a default window
        window = SDL.SDL_CreateWindow(
            "Untitled application",
            SDL.SDL_WINDOWPOS_CENTERED,                  // Window position (X)
            SDL.SDL_WINDOWPOS_CENTERED,                 // Window position (Y)
            960,                                        // Size (H)
            540,                                        // Size (W)
            SDL.SDL_WindowFlags.SDL_WINDOW_RESIZABLE    // Flags
            );

        // Check window was created successfully
        if (window == IntPtr.Zero)
        {
            Console.WriteLine($"There was an issue creating the window. {SDL.SDL_GetError()}");
        } else PawsLogger.Info("Window has been created without issues.");

        // Creates a new SDL hardware renderer using the default graphics device with VSync enabled
        renderer = SDL.SDL_CreateRenderer(
            window,
            -1,
            SDL.SDL_RendererFlags.SDL_RENDERER_ACCELERATED |
            SDL.SDL_RendererFlags.SDL_RENDERER_PRESENTVSYNC);

        if (renderer == IntPtr.Zero)
        {
            PawsLogger.Error($"There was an issue creating the renderer. {SDL.SDL_GetError()}");
        } else PawsLogger.Info("Created renderer without any issues.");

        SDL.SDL_SetRenderDrawBlendMode(renderer, SDL.SDL_BlendMode.SDL_BLENDMODE_BLEND);
    }

    /// <summary>
    /// Check for any events that have happened and handle them.
    /// </summary>
    void PollEvents()
    {
        // Check to see if there are any events and continue to do so until the queue is empty.
        while (SDL.SDL_PollEvent(out SDL.SDL_Event e) == 1)
        {
            switch (e.type)
            {
                case SDL.SDL_EventType.SDL_QUIT:
                    Application.Quit();
                    break;
            }
        }
    }

    /// <summary>
    /// Renders to the window.
    /// </summary>
    void Render()
    {
        // Sets the color that the screen will be cleared with.
        SDL.SDL_SetRenderDrawColor(renderer, 135, 206, 235, 255);

        // Clears the current render surface.
        SDL.SDL_RenderClear(renderer);

        // // Set the color to red before drawing our shape
        // SDL.SDL_SetRenderDrawColor(renderer, 255, 0, 0, 255);

        // // Draw a line from top left to bottom right
        // SDL.SDL_RenderDrawLine(renderer, 0, 0, 640, 480);

        // // Draws a point at (20, 20) using the currently set color.
        // SDL.SDL_RenderDrawPoint(renderer, 20, 20);

        // // Specify the coordinates for our rectangle we will be drawing.
        // var rect = new SDL.SDL_Rect
        // {
        //     x = 300,
        //     y = 100,
        //     w = 50,
        //     h = 50
        // };


        // Draw all the sprites
        foreach (var sprite in sprites)
        {
            // Draw the sprite
            DrawTexture(sprite.Texture, sprite.X, sprite.Y, sprite.SizeX, sprite.SizeY);
        }

        // // Draw a filled in rectangle.
        // SDL.SDL_RenderFillRect(renderer, ref rect);

        // Switches out the currently presented render surface with the one we just did work on.
        SDL.SDL_RenderPresent(renderer);
    }

    /// <summary>
    /// Clean up all the SDL resources
    /// </summary>
    void CleanUp()
    {
        // Unload all the textures
        foreach (var texture in textures)
        {
            PawsLogger.Info($"Destroyed texture {texture}.");
            SDL.SDL_DestroyTexture(texture.Value);
        }

        SDL.SDL_DestroyRenderer(renderer);
        PawsLogger.Info("Destroyed renderer.");
        SDL.SDL_DestroyWindow(window);
        PawsLogger.Info("Destroyed Window.");
        SDL.SDL_Quit();
        PawsLogger.Info("Quit SDL.");
    }
}
