using Shop.Model;

namespace Common
{
    public static class Tree
    {
        private List<Feature> GetParentFeatures(Category? category)
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

    }
}