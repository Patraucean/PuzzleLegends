﻿// // ©2015 - 2024 Candy Smith
// // All rights reserved
// // Redistribution of this software is strictly not allowed.
// // Copy of this software can be obtained from unity asset store only.
// // THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// // IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// // FITNESS FOR A PARTICULAR PURPOSE AND NON-INFRINGEMENT. IN NO EVENT SHALL THE
// // AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// // LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// // OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// // THE SOFTWARE.

using UnityEngine;

namespace BlockPuzzleGameToolkit.Scripts.Settings
{
    public class DebugSettings : SettingsBase
    {
        [Header("Debug hotkeys")]
        public bool enableHotkeys = true;

        [Tooltip("press to win")]
        public KeyCode Win;

        [Tooltip("press to lose")]
        public KeyCode Lose;

        [Tooltip("android's back button")]
        public KeyCode Back;

        [Header("")]
        [Tooltip("Test language, only for editor")]
        public SystemLanguage TestLanguage = SystemLanguage.English;
    }
}