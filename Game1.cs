using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Galaxy_TD;

public class Game1 : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;

    // Menu object
    private SpriteFont _font;
    private Texture2D _buttonTexture;
    private Rectangle startButtonRect, skillButtonRect, settingButtonRect;
    private MouseState _currentMouse, _previousMouse;

    private enum GameState { Menu, Start, Skill, Settings }
    private GameState _currentState = GameState.Menu;

    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        // TODO: Add your initialization logic here

        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        // Load phông chữ và texture cho button
        _font = Content.Load<SpriteFont>("MenuFont");
        _buttonTexture = new Texture2D(GraphicsDevice, 200, 50);
        Color[] data = new Color[200 * 50];
        for (int i = 0; i < data.Length; ++i) data[i] = Color.Gray;
        _buttonTexture.SetData(data);

        // Định vị các nút
        startButtonRect = new Rectangle(300, 150, 200, 50);
        skillButtonRect = new Rectangle(300, 250, 200, 50);
        settingButtonRect = new Rectangle(300, 350, 200, 50);
    }

    protected override void Update(GameTime gameTime)
    {
        _currentMouse = Mouse.GetState();
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
            Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        if (_currentState == GameState.Menu)
            HandleMenuInput();

        _previousMouse = _currentMouse;
        base.Update(gameTime);
    }

    private void HandleMenuInput()
    {
        // Kiểm tra nếu người dùng click vào các nút
        if (IsButtonClicked(startButtonRect)) _currentState = GameState.Start;
        else if (IsButtonClicked(skillButtonRect)) _currentState = GameState.Skill;
        else if (IsButtonClicked(settingButtonRect)) _currentState = GameState.Settings;
    }

    private bool IsButtonClicked(Rectangle buttonRect)
    {
        return buttonRect.Contains(_currentMouse.Position) &&
               _currentMouse.LeftButton == ButtonState.Pressed &&
               _previousMouse.LeftButton == ButtonState.Released;
    }

     protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);

        _spriteBatch.Begin();

        if (_currentState == GameState.Menu)
        {
            DrawButton(startButtonRect, "Start");
            DrawButton(skillButtonRect, "Skill");
            DrawButton(settingButtonRect, "Settings");
        }
        else
        {
            _spriteBatch.DrawString(_font, $"State: {_currentState}", new Vector2(300, 200), Color.White);
        }

        _spriteBatch.End();
        base.Draw(gameTime);
    }

    private void DrawButton(Rectangle rect, string text)
    {
        _spriteBatch.Draw(_buttonTexture, rect, Color.White);
        Vector2 textSize = _font.MeasureString(text);
        Vector2 textPosition = new Vector2(
            rect.X + (rect.Width - textSize.X) / 2,
            rect.Y + (rect.Height - textSize.Y) / 2
        );
        _spriteBatch.DrawString(_font, text, textPosition, Color.Black);
    }
}
