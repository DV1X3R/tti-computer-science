import random
import numpy as np
import pandas as pd
import seaborn as sns
import matplotlib.pyplot as plt
import imageio
from sklearn.cluster import KMeans


class _KMeans:

    def __init__(self, n_clusters=8, init='k-means++', max_iter=300):
        self.n_clusters = n_clusters
        self.init = init
        self.max_iter = max_iter
        self.x = None
        self.cluster_centers_ = None
        self.cluster_centers_old_ = None
        self.labels_ = None

    def fit(self, x):
        iter = 0
        self.x = np.array(x)
        self._init_centroids()  # Step 0
        while not np.array_equal(self.cluster_centers_old_, self.cluster_centers_) \
                and iter < self.max_iter:
            iter += 1
            self.cluster_centers_old_ = self.cluster_centers_.copy()
            self._calculate_labels()  # Step 1
            self._calculate_centroids()  # Step 2
        return self

    def _init_centroids(self):
        if self.init == 'k-means++':
            # first random centroid
            x_pool = self.x.copy()
            c = np.random.randint(len(x_pool))
            self.cluster_centers_ = [x_pool[c]]
            x_pool = np.delete(x_pool, c, axis=0)

            # remaining centroids
            while len(self.cluster_centers_) < self.n_clusters:
                # calculated distances and probabilities (max dist -> larger prob)
                d = [self._calculate_distance(
                    self.cluster_centers_[-1], x) for x in x_pool]
                p = d / sum(np.power(d, 2))
                # picking best centroid
                centroid = random.choices(range(len(p)), p)[0]
                self.cluster_centers_ = np.append(
                    self.cluster_centers_, [x_pool[centroid]], axis=0)
                x_pool = np.delete(x_pool, centroid, axis=0)

        else:  # pick n random centroids
            self.cluster_centers_ = \
                [self.x[i] for i in random.sample(
                    range(len(self.x)), self.n_clusters)]

    def _calculate_labels(self):
        d = [[self._calculate_distance(i, c)
              for c in self.cluster_centers_] for i in self.x]
        #d = self._calculate_distance(self.x, self.cluster_centers_)
        self.labels_ = np.argmin(d, axis=1)  # indexes of min values

    def _calculate_centroids(self):
        centroids = []
        for c in range(self.n_clusters):
            x = [self.x[li] for li, lc in enumerate(self.labels_) if lc == c]
            x = np.array(x).T  # make a list of one type variable values
            centroids.append([sum(i) / len(i) for i in x])
        self.cluster_centers_ = np.array(centroids)

    def _calculate_distance(self, x1, x2):  # Euclidean distance
        return np.sqrt(sum(np.power(x1 - x2, 2)))


def show_test():
    X = pd.DataFrame({
        'x': [1, 2, 1, 3, 5, 4],
        'y': [5, 5, 4, 2, 2, 1]
    })
    X['k'] = _KMeans(n_clusters=2).fit(X).labels_
    sns.scatterplot(x=X['x'], y=X['y'], hue=X['k'])
    plt.show()


def show_iris():

    X = pd.read_csv('iris.data.csv')
    X['k'] = _KMeans(n_clusters=3).fit(
        X[['sepal length', 'sepal width', 'petal length', 'petal width']]).labels_
    sns.pairplot(X, vars=['sepal length', 'sepal width',
                          'petal length', 'petal width'], hue='k')
    # sns.pairplot(X, vars=['sepal length', 'sepal width', 'petal length', 'petal width'], hue='class')
    plt.show()


def compress_image():
    X = imageio.imread('image.jpg')
    k = _KMeans(n_clusters=2).fit(np.reshape(X, (-1, 3)))
    c = list(map((lambda x: k.cluster_centers_[x].astype(int)), k.labels_))
    plt.imshow(np.reshape(c, X.shape))
    plt.show()


# show_test()
# show_iris()
# compress_image()
