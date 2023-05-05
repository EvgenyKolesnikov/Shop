using Shop.Model;

namespace ShopApp.Common
{
    public static class Tree
    {
        public static List<Feature> GetParentFeatures(Category? category)
        {
            var features = new List<Feature>();

            var _category = category;

            while (_category != null)
            {
                features.AddRange(_category.Features);
                _category = _category.ParentCategory;
            }

            return features;
        }


        public static List<Category> GetChildrenCategories(List<Category?> categories)
        {
            var neededToVisit = new List<Category>();
            var ChildrenCategories = new List<Category>();
            neededToVisit.AddRange(categories);

            while (neededToVisit.Count > 0)
            {
                var current = neededToVisit.First();

                ChildrenCategories.Add(current);

                foreach (var child in current.ChildCategories ?? new List<Category>())
                {
                    neededToVisit.Add(child);
                }
                neededToVisit.Remove(current);
            }
            return ChildrenCategories;
        }

    }
}
