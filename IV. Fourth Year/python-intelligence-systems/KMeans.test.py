# %%
from KMeans import _KMeans
from sklearn.cluster import KMeans
import pandas as pd
import seaborn as sns
import matplotlib.pyplot as plt


X = pd.DataFrame({
    'x': [1, 2, 1, 3, 5, 4],
    'y': [5, 5, 4, 2, 2, 1]
})
X['k'] = _KMeans(n_clusters=2).fit(X).labels_
sns.scatterplot(x=X['x'], y=X['y'], hue=X['k'])

X = pd.read_csv('iris.data.csv')
X['k'] = _KMeans(n_clusters=3).fit(
    X[['sepal length', 'sepal width', 'petal length', 'petal width']]).labels_
sns.pairplot(X, vars=['sepal length', 'sepal width',
                      'petal length', 'petal width'], hue='k')
# sns.pairplot(X, vars=['sepal length', 'sepal width', 'petal length', 'petal width'], hue='class')

plt.show()
# plt.figure()
