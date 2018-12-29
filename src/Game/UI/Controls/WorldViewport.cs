﻿#region license
//  Copyright (C) 2018 ClassicUO Development Community on Github
//
//	This project is an alternative client for the game Ultima Online.
//	The goal of this is to develop a lightweight client considering 
//	new technologies.  
//      
//  This program is free software: you can redistribute it and/or modify
//  it under the terms of the GNU General Public License as published by
//  the Free Software Foundation, either version 3 of the License, or
//  (at your option) any later version.
//
//  This program is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//  GNU General Public License for more details.
//
//  You should have received a copy of the GNU General Public License
//  along with this program.  If not, see <https://www.gnu.org/licenses/>.
#endregion

using System.Linq;

using ClassicUO.Game.Scenes;
using ClassicUO.Game.UI.Gumps;
using ClassicUO.Input;
using ClassicUO.Renderer;

using Microsoft.Xna.Framework;

namespace ClassicUO.Game.UI.Controls
{
    internal class WorldViewport : Control
    {
        private readonly GameScene _scene;
        private Rectangle _rect;

        public WorldViewport(GameScene scene, int x, int y, int width, int height)
        {
            X = x;
            Y = y;
            Width = width;
            Height = height;
            _scene = scene;
            AcceptMouseInput = true;
        }

        public override bool Draw(Batcher2D batcher, Point position, Vector3? hue = null)
        {
            _rect.X = position.X;
            _rect.Y = position.Y;
            _rect.Width = Width;
            _rect.Height = Height;
            batcher.Draw2D(_scene.ViewportTexture, _rect, Vector3.Zero);

            return base.Draw(batcher, position, hue);
        }

        protected override void OnMouseClick(int x, int y, MouseButton button)
        {
            if (!(Engine.UI.KeyboardFocusControl is TextBox tb && tb.RootParent is WorldViewportGump))
            {
                Engine.UI.KeyboardFocusControl = Parent
                                                       .FindControls<SystemChatControl>()
                                                       .FirstOrDefault()?
                                                       .GetFirstControlAcceptKeyboardInput();
            }
        }
    }
}