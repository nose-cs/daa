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


def greedy_dominating_set_V_plus_E(graph):
    WHITE = 0
    GRAY = 1
    BLACK = 2

    color = {}  # Color of each node: white, gray, or black
    count = {}  # Number of adjacent white nodes for each node
    buckets = {}  # Buckets to group nodes by their count of adjacent white nodes

    # Find the maximum degree in the graph
    max_degree = max(len(neighbors) for neighbors in graph.values())
    n = len(graph)
    white_nodes_remaining = n

    # Initialize buckets for counts from 0 to max_degree
    for c in range(max_degree + 2):
        buckets[c] = set()

    # Initialize colors and counts, and fill the buckets
    for u in graph:
        color[u] = WHITE
        count_u = len(graph[u]) + 1
        count[u] = count_u
        buckets[count_u].add(u)

    max_count = max(count.values())
    dominating_set = set()

    # Main loop to select nodes for the dominating set
    while white_nodes_remaining > 0:
        # Find the node with the maximum count of adjacent white nodes
        while max_count > 0 and not buckets[max_count]:
            max_count -= 1
        if max_count <= 0:
            break  # No nodes left to process
        
        # There are white nodes but there aren´t connections to them
        if max_count == 0:
            for node, c in color.items():
                if c == WHITE:
                    dominating_set.add(node)
            
            return len(dominating_set)

        u = buckets[max_count].pop()
        
        if color[u] == BLACK:
            continue  # Skip if the node is already black
        
        u_was_WHITE = False
        if color[u] == WHITE:
            white_nodes_remaining -= 1
            u_was_WHITE = True
        
        color[u] = BLACK
        dominating_set.add(u)

        # Update the colors and counts of adjacent nodes
        for v in graph[u]:
            if color[v] != BLACK and u_was_WHITE:
                old_count = count[v]
                
                if v in buckets[old_count]:
                    buckets[old_count].remove(v)
                
                count[v] -= 1
                new_count = count[v]
                buckets[new_count].add(v)

            if color[v] == WHITE:
                color[v] = GRAY
                white_nodes_remaining -= 1
                
                old_count = count[v]
                
                if v in buckets[old_count]:
                    buckets[old_count].remove(v)
                
                count[v] -= 1
                new_count = count[v]
                buckets[new_count].add(v)

                for w in graph[v]:
                    if color[w] != BLACK:
                        old_count = count[w]
                        
                        if w in buckets[old_count]:
                            buckets[old_count].remove(w)
                        
                        count[w] -= 1
                        new_count = count[w]
                        buckets[new_count].add(w)
            
    return len(dominating_set)