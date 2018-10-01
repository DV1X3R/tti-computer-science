#! Random variable generators.

import scipy.stats as st


def normal_rand(size=1):
    # loc = μ [mu] (mean).
    # scale = σ^2 [sigma] (variance).
    return st.norm.rvs(loc=12, scale=2, size=size)


def normal_pdf(rng):
    return st.norm.pdf(loc=12, scale=2, x=rng)


def exponential_rand(size=1):
    # scale = 1/λ [lambda].
    return st.expon.rvs(scale=1.0 / 2, size=size)


def exponential_pdf(rng):
    return st.expon.pdf(scale=1.0 / 2, x=rng)


def erlang_rand(size=1):
    # a = shape.
    # scale = 1/λ [lambda].
    return st.erlang.rvs(a=3, scale=1 / 0.25, size=size)


def erlang_pdf(rng):
    return st.erlang.pdf(a=3, scale=1 / 0.25, x=rng)


def poisson_rand(size=1):
    # mu = λ [lambda].
    # loc = mean.
    return st.poisson.rvs(mu=10, loc=0, size=size)


def poisson_pmf(rng):
    return st.poisson.pmf(mu=10, loc=0, k=rng)
