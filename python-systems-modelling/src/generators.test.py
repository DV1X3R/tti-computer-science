#! Basic distribution fitting test.

from generators import *
from numpy import arange
import matplotlib.pyplot as plt


size = 1000
plt.suptitle('Random Generator Test (' + str(size) + ' cases)', fontweight='bold')

# Normal Distribution.
rng = arange(2, 22, 0.1)

plt.subplot(221)
plt.hist(normal_rand(size), bins=20, density=True, color='C0', label='RAND', zorder=0)
plt.plot(rng, normal_pdf(rng), color='C3', label='PDF', zorder=1)
plt.title('Normal distribution')
plt.xlabel('x')
plt.ylabel('f(x)')
plt.legend()


# Exponential Distribution.
rng = arange(0, 5, 0.1)

plt.subplot(222)
plt.hist(exponential_rand(size), bins=20, density=True, color='C0', label='RAND', zorder=0)
plt.plot(rng, exponential_pdf(rng), color='C3', label='PDF', zorder=1)
plt.title('Exponential distribution')
plt.xlabel('x')
plt.ylabel('f(x)')
plt.legend()


# Erlang Distribution.
rng = arange(0, 50, 0.1)

plt.subplot(223)
plt.hist(erlang_rand(size), bins=30, density=True, color='C0', label='RAND', zorder=0)
plt.plot(rng, erlang_pdf(rng), color='C3', label='PDF', zorder=1)
plt.title('Erlang distribution')
plt.xlabel('x')
plt.ylabel('f(x)')
plt.legend()


# Poisson Distribution.
rng = range(19)

plt.subplot(224)
plt.hist(poisson_rand(size), bins=19, density=True, color='C0', label='RAND', zorder=0)
plt.plot(rng, poisson_pmf(rng), color='C3', zorder=1)
plt.scatter(rng, poisson_pmf(rng), color='C3', label='PMF', zorder=2)
plt.title('Poisson distribution')
plt.xlabel('x')
plt.ylabel('f(x)')
plt.legend()

plt.show()
