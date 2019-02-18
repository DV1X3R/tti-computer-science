# %%
import numpy as np
import pandas as pd
import seaborn as sns
import matplotlib.pyplot as plt
from sklearn.cluster import KMeans
from math import sqrt, pow


class _KMeans:

    def __init__(self, n_clusters, init='k-means++'):
        self.n_clusters = n_clusters
        self.init = init
        self.labels_ = []  # вектор кластерной принадлежности

    def fit(self, x):
        if isinstance(x, pd.DataFrame):
            x = x.values
        
        x_rows = len(x)  # количество наблюдений
        x_variables = len(x[0])  # количество значимых переменных
        
        u_clusters_prev = []
        n_clusters = self.n_clusters  # количество кластеров
        init = self.init  # алгоритм

        # начальные центроиды кластеров
        if init == 'k-means++':
            u_clusters = [x for i, x in enumerate(x) if i < n_clusters]
        else:
            u_clusters = [x for i, x in enumerate(x) if i < n_clusters]

        while np.array_equal(u_clusters, u_clusters_prev) == False:
            u_clusters_prev = u_clusters.copy()

            # STEP 1: меры расстояния от наблюдения до центроидов
            d_clusters = [[
                sqrt(sum(
                    [pow(x[i][v] - u_clusters[c][v], 2)
                     for v in range(x_variables)]
                )) for c in range(n_clusters)] for i in range(x_rows)]

            # вектор кластерной принадлежности
            self.labels_ = [np.argmin(d_clusters[i]) for i in range(x_rows)]

            # STEP 2: новые центроиды кластеров
            u_clusters.clear()
            for i in range(n_clusters):
                ic = [li for li, lc in enumerate(
                    self.labels_) if lc == i]  # индексы

                dc = [[x[i][v] for i in ic]
                      for v in range(x_variables)]  # значения

                u_clusters.append([sum(dc[i]) / len(dc[i])
                                   for i in range(len(dc))])

        return self


# %%
X = pd.DataFrame({
    'x': [1, 2, 1, 3, 5, 4],
    'y': [5, 5, 4, 2, 2, 1]
})
X['k'] = _KMeans(n_clusters=2).fit(X).labels_
sns.scatterplot(x=X['x'], y=X['y'], hue=X['k'])

# %%
X = pd.read_csv('iris.data.csv')
X['k'] = _KMeans(n_clusters=3).fit(
    X[['sepal length', 'sepal width', 'petal length', 'petal width']]).labels_
sns.pairplot(X, vars=['sepal length', 'sepal width',
                      'petal length', 'petal width'], hue='k')
# sns.pairplot(X, vars=['sepal length', 'sepal width', 'petal length', 'petal width'], hue='class')

plt.show()
# plt.figure()
