# Introduction to Intelligence Systems (4 year / 2 semester)

## Exercise 1 - Кластеризация методом 𝒌 средних
* /KMeans.ipynb
* [sklearn.cluster.KMeans](https://scikit-learn.org/stable/modules/generated/sklearn.cluster.KMeans.html#sklearn.cluster.KMeans)

Минимум:
* реализация, работающая на наборе данных «цветы ириса»
* подсчет доли верно определенных классов (accuracy)  

Улучшения:
* визуализация результатов (matplotlib.pyplot.scatter),
* сжатие изображений (нахождение «центров тяжести» в пространстве цветов),
* \*\*векторизованная реализация (и сравнение с циклической реализацией),
* инициализация 𝑘 средних++,
* \*векторизованная инициализация 𝑘 средних++.

## Exercise 2 - Перцептрон
* /Perceptron.ipynb
* [sklearn.linear_model.Perceptron](https://scikit-learn.org/stable/modules/generated/sklearn.linear_model.Perceptron.html)

Минимум:
* реализация, работающая на наборе данных «цветы ириса» (линейно разделимые классы: Iris-setosa, Iris-versicolor),
* подбор коэффициента обучения 𝛼,
* подсчет доли верно определенных классов (accuracy) на обучающей (80% данных) и валидационной (20% данных) выборках.  

Улучшения:
* векторизованная реализация,
* визуализация результатов.

## Notes

Libraries
```
# numpy - работа с матрицами
# scipy - научные вычисления
# pandas - надстройка над numpy: матрицы в виде таблиц
# scikit-learn - машинное обучение
# matplotlib - работа с графиками
# seaborn - надстройка над matplotlib: стандартные графики
# jupyter - сервер вычислений

pip3 install numpy scipy pandas scikit-learn matplotlib seaborn jupyter
jupyter notebook
```

* [Towards Data Science](https://towardsdatascience.com/)
* [dataquest.io](https://www.dataquest.io/)
* [python-course.eu](https://www.python-course.eu/numerical_programming_with_python.php)
* [Pandas_Cheat_Sheet.pdf](http://pandas.pydata.org/Pandas_Cheat_Sheet.pdf)
* [10 Minutes to pandas](https://pandas.pydata.org/pandas-docs/stable/getting_started/10min.html)
* [Pandas Tutorial: DataFrames in Python 'datacamp.com'](https://www.datacamp.com/community/tutorials/pandas-tutorial-dataframe-python)
* [List Comprehensions in Python](https://www.pythonforbeginners.com/basics/list-comprehensions-in-python)
* [Использование точечных диаграмм для визуализации данных 'habr.com'](https://habr.com/ru/post/440674/)
* [Data Science в Visual Studio Code с использованием Neuron 'habr.com'](https://habr.com/ru/company/microsoft/blog/428738/)
* [Loading A CSV Into pandas](https://chrisalbon.com/python/data_wrangling/pandas_dataframe_importing_csv/)
* [NumPy - Broadcasting](https://www.tutorialspoint.com/numpy/numpy_broadcasting.htm)
* [SciPy - Broadcasting](https://docs.scipy.org/doc/numpy/user/basics.broadcasting.html)
* [SciPy - Indexing](https://docs.scipy.org/doc/numpy-1.13.0/reference/arrays.indexing.html)
* [Векторизация](http://slemeshevsky.github.io/python-num-pde/term2/build/html/_fdm-for-wave/vectorization.html)
