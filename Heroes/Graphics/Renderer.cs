using Heroes.Graphics.Renderers.SDL2.Bindings;
using Heroes.Debug.Paws;

namespace Heroes.Graphics.Renderers.SDL2;

public class Renderer
{
    IntPtr window;
    IntPtr renderer;

    private List<Sprite> sprites = new();

    static public Renderer? Instance;

    private bool cleanedUp = false;

    private SDLColour backgroundColour = new(0, 0, 0, 255);

    // List of textures that have been loaded
    Dictionary<string, IntPtr> textures = new();

    // Queue of textures to load
    Queue<(string path, Sprite sprite)> texturesToLoad = new();

    // List of all texture ID's
    List<int> textureIDs = new();

    public Renderer()
    {
        Instance = this;
    }

    ~Renderer()
    {
        if (!cleanedUp) CleanUp();
    }

    /// <summary> Enqueue a texture to be loaded and SpriteData to be modified. </summary>
    /// <param name="path"> The path to the texture. </param>
    /// <param name="sprite"> The SpriteData to modify. </param>
    internal void EnqueueTexture(string path, Sprite sprite)
    {
        // Debug log enqueued
        PawsLogger.Debug($"Enqueued texture: {path}");

        // Add the texture to the queue
        texturesToLoad.Enqueue((path, sprite));
    }

    /// <summary> Add a sprite to the renderer (once loaded). </summary>
    /// <param name="sprite"> The sprite to add. </param>
    internal void AddSprite(Sprite sprite)
    {
        sprites.Add(sprite);
    }

    /// <summary> Remove a sprite from the renderer. </summary>
    /// <param name="sprite"> The sprite to remove. </param>
    internal void RemoveSprite(Sprite sprite)
    {
        sprites.Remove(sprite);
    }

    /// <summary> Update a sprite in the renderer. </summary>
    /// <param name="sprite"> The sprite to update. </param>
    internal void UpdateSprite(Sprite sprite)
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

    /// <summary> Load an image into memory and return a pointer to it. </summary>
    /// <param name="path"> The path to the image. </param>
    /// <returns> A pointer to the image. </returns>
    internal IntPtr LoadImage(string path)
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

        // While the game loop is running, poll for events and render.
        while (Heroes.Internal.GameLoop.Running)
        {
            PollEvents();
            Render();
            LoadQueuedTextures();
        }

        // Clean up all of the SDL resources
        CleanUp();
    }

    private void LoadQueuedTextures()
    {
        // Check if there are any textures to load
        if (texturesToLoad.Count == 0) return;

        // Load all of the textures in the queue
        while (texturesToLoad.Count > 0)
        {
            // Get the texture to load
            var textureToLoad = texturesToLoad.Dequeue();

            // Load the texture
            var texture = LoadImage(textureToLoad.path);

            // Check if the texture was loaded successfully
            if (texture == IntPtr.Zero)
            {
                PawsLogger.Error($"There was an issue loading the texture {textureToLoad.path}.");
                continue;
            }

            // Update the texture on the sprite
            textureToLoad.sprite.Texture = texture;

            // Log message
            PawsLogger.Info($"Loaded texture {textureToLoad.path} and updated the sprite with it.");
        }
    }

    /// <summary> Set the background colour </summary>
    /// <param name="colour"> The colour to set the background to. </param>
    public void SetBackgroundColour(SDLColour colour)
    {
        backgroundColour = colour;
    }

    /// <summary> Get the background colour. </summary>
    /// <returns> The background colour. </returns>
    public SDLColour GetBackgroundColour()
    {
        return backgroundColour;
    }
    

    /// <summary> Set the window title. </summary>
    internal bool WindowGetFullscreen()
    {
        // Check if the window is fullscreen
        if (SDL.SDL_GetWindowFlags(window) == (uint)SDL.SDL_WindowFlags.SDL_WINDOW_FULLSCREEN)
            return true;
        else
            return false;
    }

    /// <summary> Set the window to fullscreen or exit fullscreen. </summary>
    internal void WindowSetFullscreen(bool toFullscreen = true)
    {
        if (!toFullscreen)
        {
            // Exit fullscreen
            SDL.SDL_SetWindowFullscreen(window, 0);
        }
        else
        {
            // Make the window fullscreen
            SDL.SDL_SetWindowFullscreen(window, (uint)SDL.SDL_WindowFlags.SDL_WINDOW_FULLSCREEN);
            // Get display size and set the window size to the display size
            SDL.SDL_GetDisplayBounds(0, out SDL.SDL_Rect rect);
            WindowSetSize(rect.w, rect.h);
        }
    }

    /// <summary> Get the size of the window. </summary>
    /// <returns> The size of the window as an int array. </returns>
    internal int[] WindowGetSize()
    {
        SDL.SDL_GetWindowSize(window, out int w, out int h);
        return new int[] { w, h };
    }

    /// <summary> Set the size of the window. </summary>
    /// <param name="width"> The width of the window. </param>
    /// <param name="height"> The height of the window. </param>
    internal void WindowSetSize(int width, int height)
    {
        SDL.SDL_SetWindowSize(window, width, height);
    }

    /// <summary> Return the title of the window. </summary>
    /// <returns> The title of the window. </returns>
    internal string WindowGetTitle()
    {
        return SDL.SDL_GetWindowTitle(window);
    }

    /// <summary> Set the title of the window. </summary>
    /// <param name="windowTitle"> The title of the window. </param>
    internal void WindowSetTitle(string windowTitle)
    {
        SDL.SDL_SetWindowTitle(window, "Heroes");
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
        }
        else PawsLogger.Info("SDL has been initialised without issues.");

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
        }
        else PawsLogger.Info("Window has been created without issues.");

        // Creates a new SDL hardware renderer using the default graphics device with VSync enabled
        renderer = SDL.SDL_CreateRenderer(
            window,
            -1,
            SDL.SDL_RendererFlags.SDL_RENDERER_ACCELERATED |
            SDL.SDL_RendererFlags.SDL_RENDERER_PRESENTVSYNC);

        if (renderer == IntPtr.Zero)
        {
            PawsLogger.Error($"There was an issue creating the renderer. {SDL.SDL_GetError()}");
        }
        else PawsLogger.Info("Created renderer without any issues.");

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
        SDL.SDL_SetRenderDrawColor(renderer, backgroundColour.R, backgroundColour.G, backgroundColour.B, backgroundColour.A);

        // Clears the current render surface.
        SDL.SDL_RenderClear(renderer);


        // Draw all the sprites
        foreach (var sprite in sprites)
        {
            // Draw the sprite
            DrawTexture(sprite.Texture, sprite.X, sprite.Y, sprite.SizeX, sprite.SizeY);
        }

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
