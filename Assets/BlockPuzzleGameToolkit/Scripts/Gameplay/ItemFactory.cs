// // ©2015 - 2024 Candy Smith
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

using System.Collections.Generic;
using System.Linq;
using BlockPuzzleGameToolkit.Scripts.Enums;
using BlockPuzzleGameToolkit.Scripts.LevelsData;
using UnityEngine;

namespace BlockPuzzleGameToolkit.Scripts.Gameplay
{
    public class ItemFactory : MonoBehaviour
    {
        private ShapeTemplate[] shapes;
        protected ItemTemplate[] items;
        private Level level;
        private Dictionary<BonusItemTemplate, int> predictedTargets;
        public bool _oneColorMode;
        protected int _oneColor;

        [SerializeField]
        private FieldManager field;

        [SerializeField]
        private CellDeckManager cellDeck;

        [SerializeField]
        private TargetManager targetManager;

        [SerializeField]
        private LevelManager levelManager;

        protected virtual void Awake()
        {
            // load shapes from resources
            shapes = Resources.LoadAll<ShapeTemplate>("Shapes");
            // load items from resources
            items = Resources.LoadAll<ItemTemplate>("Items");
        }

        private ShapeTemplate GetNonRepeatedShapeTemplate(HashSet<ShapeTemplate> usedShapeTemplates)
        {
            ShapeTemplate shapeTemplate = null;
            if (usedShapeTemplates == null)
            {
                return GetRandomShape();
            }

            do
            {
                shapeTemplate = GetRandomShape();
            } while (usedShapeTemplates.Contains(shapeTemplate));

            usedShapeTemplates.Add(shapeTemplate);
            return shapeTemplate;
        }

        private ShapeTemplate GetRandomShape()
        {
            ShapeTemplate shapeTemplate = null;
            var shapesToConsider = levelManager.GetGameMode() == EGameMode.Adventure
                ? shapes.Where(shape => shape.spawnFromLevel <= levelManager.currentLevel).ToArray()
                : shapes.Where(shape => shape.scoreForSpawn <= GetClassicScore()).ToArray();

            var totalWeight = shapesToConsider.Sum(shape => shape.chanceForSpawn);
            var randomWeight = Random.Range(0, totalWeight);

            foreach (var shape in shapesToConsider)
            {
                if (randomWeight < shape.chanceForSpawn)
                {
                    shapeTemplate = shape;
                    break;
                }

                randomWeight -= shape.chanceForSpawn;
            }

            return shapeTemplate;
        }

        private static int GetClassicScore()
        {
            return FindObjectOfType<ClassicModeHandler>()?.score ?? 0;
        }

        public Shape CreateRandomShape(HashSet<ShapeTemplate> usedShapeTemplates, GameObject shapeObject)
        {
            var shape = shapeObject.GetComponent<Shape>();
            shape.transform.localScale = Vector3.one;
            shape.UpdateShape(GetNonRepeatedShapeTemplate(usedShapeTemplates));

            var currentTargets = targetManager.GetTargets();
            if (currentTargets != null && currentTargets.Any(i => i.targetScriptable.bonusItem != null))
            {
                GenerateBonus(shape, currentTargets);
            }

            shape.UpdateColor(GetColor());

            return shape;
        }

        public Shape CreateRandomShapeFits(GameObject shapeObject)
        {
            var shape = shapeObject.GetComponent<Shape>();
            shape.UpdateShape(GetRandomShape());
            var shapes_random = shapes.OrderBy(x => Random.value).ToList();
            foreach (var shapeTemplate in shapes_random)
            {
                shape.UpdateShape(shapeTemplate);
                shape.UpdateColor(GetColor());
                if (field.CanPlaceShape(shape))
                {
                    return shape;
                }
            }

            return shape;
        }

        public ItemTemplate GetColor()
        {
            return !_oneColorMode ? items[Random.Range(1, items.Length)] : items[_oneColor];
        }

        public ItemTemplate GetOneColor()
        {
            return items[_oneColor];
        }

        private void GenerateBonus(Shape shapeObject, List<Target> targets)
        {
            var predictedTargets = new Dictionary<BonusItemTemplate, int>(targets.Count);
            foreach (var target in targets)
            {
                predictedTargets[target.targetScriptable.bonusItem] = target.amount;
            }

            // Count the amount of targets already on the field cells
            var fieldCells = field.GetAllCells();
            foreach (var cell in fieldCells)
            {
                if (cell.HasBonusItem())
                {
                    var bonusItem = cell.GetBonusItem();
                    if (predictedTargets.ContainsKey(bonusItem))
                    {
                        predictedTargets[bonusItem]--;
                        if (predictedTargets[bonusItem] <= 0)
                        {
                            predictedTargets.Remove(bonusItem);
                        }
                    }
                }
            }

            // get bonuses on deck
            var shapesOnDeck = cellDeck.GetShapes();
            foreach (var shape in shapesOnDeck)
            {
                foreach (var item in shape.GetActiveItems())
                {
                    if (item.HasBonusItem())
                    {
                        var bonusItem = item.bonusItemTemplate;
                        if (predictedTargets.ContainsKey(bonusItem))
                        {
                            predictedTargets[bonusItem]--;
                            if (predictedTargets[bonusItem] <= 0)
                            {
                                predictedTargets.Remove(bonusItem);
                            }
                        }
                    }
                }
            }

            var keys = predictedTargets.Keys.ToList();
            keys = keys.OrderBy(x => Random.value).ToList();

            foreach (var key in keys)
            {
                if (predictedTargets[key] > 0 && Random.Range(0, 3) == 0)
                {
                    shapeObject.SetBonus(key, predictedTargets[key]);
                    return;
                }
            }
        }
    }
}