from solution import build_tree

x = int(input("Enter x: "))
y = int(input("Enter y: "))

n, edges = build_tree(x, y)

if edges is None:
    print("No such tree exists")
else:
    print(f"Tree with {n} vertices:")
    for u, v in edges:
        print(f"{u} - {v}")
