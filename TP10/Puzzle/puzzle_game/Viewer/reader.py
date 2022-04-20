from enum import Enum


class Directions(Enum):
    """
        Directions class
    """
    UP = 1
    DOWN = 2
    RIGHT = 3
    LEFT = 4

FILENAME='Viewer/steps.out'

LOOCKUP_TABLE = [
        ("UP", Directions.UP),
        ("DOWN", Directions.DOWN),
        ("RIGHT", Directions.RIGHT),
        ("LEFT", Directions.LEFT)
        ]


steps = []
tmp_steps = []

matrix = []

# WARNING !! File needs to end with a \n at the end

with open(FILENAME) as file_in:
    for idx, line in enumerate(file_in):

        if not idx:
            matrix = line[:-1].split(';')
            continue

        # If line is empty so skip it
        if (line[:-1] ==  ""):
            continue

        found = False

        for string_direct, direction in LOOCKUP_TABLE:
            if line[:-1] == string_direct :
                found = True
                steps.append(direction)
                break


        if not found:
            print(line[:-1])
            raise Exception(f"{line[:-1]}Directions Unknown")

