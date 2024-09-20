import itertools

# =============================== Solution ===============================


def is_dominating_set(subset, graph):
    dominated = set(subset)
    for node in subset:
        for edge in graph[node]:
            dominated.add(edge)
    return len(dominated) == len(graph)


def smallest_dominating_set(graph):
    n = len(graph)
    nodes = [i for i in range(n)]
    for i in range(1, n + 1):
        for subset in itertools.combinations(nodes, i):
            if is_dominating_set(subset, graph):
                return i
    return len(graph)


# =============================== Approximation ===============================


def greedy_dominating_set_without_gray(graph):
    n = len(graph)
    nodes = [i for i in range(n)]
    dominating_set = set()
    uncovered_neighbors = {node: len(graph[node]) for node in graph}
    while any([uncovered_neighbors[node] >= 0 for node in graph]):
        node = max(nodes, key=lambda node: uncovered_neighbors[node])
        dominating_set.add(node)
        uncovered_neighbors[node] = -1
        for neighbor in graph[node]:
            uncovered_neighbors[neighbor] = -1
    return len(dominating_set)


def greedy_dominating_set(graph):
    n = len(graph)
    nodes = [i for i in range(n)]
    dominating_set = set()
    dominated_mask = [False] * n
    uncovered_neighbors = {node: len(graph[node]) + 1 for node in graph}
    while any([uncovered_neighbors[node] > 0 for node in graph]):
        node = max(nodes, key=lambda node: uncovered_neighbors[node])
        dominating_set.add(node)
        uncovered_neighbors[node] -= 1
        dominated_mask[node] = True
        for neighbor in graph[node]:
            if not dominated_mask[neighbor]:
                for x in graph[neighbor]:
                    uncovered_neighbors[x] -= 1
                dominated_mask[neighbor] = True
                uncovered_neighbors[neighbor] -= 1
            uncovered_neighbors[neighbor] -= 1
    return len(dominating_set)
