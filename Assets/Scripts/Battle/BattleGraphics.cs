using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class BattleGraphics
{
    static Texture2D cursor_commands;
    public static Sprite fight, fight_highlight, pokemon, pokemon_highlight, bag, bag_highlight, run, run_highlight;

    static BattleGraphics()
    {
        cursor_commands = Resources.Load<Texture2D>("Graphics/Battle/cursor_command");
        
        fight             = Sprite.Create(cursor_commands, new Rect(0, cursor_commands.height * 9 / 10, cursor_commands.width / 2, cursor_commands.height / 10), new Vector2(0, 0), .01f);
        fight_highlight   = Sprite.Create(cursor_commands, new Rect(cursor_commands.width / 2, cursor_commands.height * 9 / 10, cursor_commands.width / 2, cursor_commands.height / 10), new Vector2(0, 0), .01f);
        pokemon           = Sprite.Create(cursor_commands, new Rect(0, cursor_commands.height * 8 / 10, cursor_commands.width / 2, cursor_commands.height / 10), new Vector2(0, 0), .01f);
        pokemon_highlight = Sprite.Create(cursor_commands, new Rect(cursor_commands.width / 2, cursor_commands.height * 8 / 10, cursor_commands.width / 2, cursor_commands.height / 10), new Vector2(0, 0), .01f);
        bag               = Sprite.Create(cursor_commands, new Rect(0, cursor_commands.height * 7 / 10, cursor_commands.width / 2, cursor_commands.height / 10), new Vector2(0, 0), .01f);
        bag_highlight     = Sprite.Create(cursor_commands, new Rect(cursor_commands.width / 2, cursor_commands.height * 7 / 10, cursor_commands.width / 2, cursor_commands.height / 10), new Vector2(0, 0), .01f);
        run               = Sprite.Create(cursor_commands, new Rect(0, cursor_commands.height * 6 / 10, cursor_commands.width / 2, cursor_commands.height / 10), new Vector2(0, 0), .01f);
        run_highlight     = Sprite.Create(cursor_commands, new Rect(cursor_commands.width / 2, cursor_commands.height * 6 / 10, cursor_commands.width / 2, cursor_commands.height / 10), new Vector2(0, 0), .01f);
    }
}
