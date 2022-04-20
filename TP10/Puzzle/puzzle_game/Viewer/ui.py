
import turtle
import tkinter as tk

import random
import math

from typing import Dict

from PIL import Image
from PIL import ImageFont
from PIL import ImageDraw

from reader import Directions, matrix, steps

# BOARD SETTINGS
NBR_TILES = len(matrix)
NUM_ROWS = int(math.sqrt(NBR_TILES))
NUM_COLS =  int(math.sqrt(NBR_TILES))

# TILE SETTINGS
TILE_WIDTH = int(360 / NUM_ROWS)  # Actual image size
TILE_HEIGHT = int(360 / NUM_COLS) # Actual image size


# FONT SETTINGS
FONT_SIZE = 24
FONT = ('Helvetica', FONT_SIZE, 'normal')
TILE_FONT_COLOR = (250, 250, 250)
TILE_BACKGROUND_COLOR = (94, 157, 159)

# WINDOW SETTINGS
WINDOW_HEIGHT = TILE_HEIGHT * NUM_ROWS + 2 * NUM_ROWS;
WINDOW_WIDTH = TILE_WIDTH * NUM_COLS + 2 * NUM_ROWS;


screen = turtle.Screen()
board = []

images = []

def create_images(nbr):
    """
        Create image from 1 to nbr
    """
    #font = ImageFont.load_default()
    font = ImageFont.truetype("fonts/OpenSans-Regular.ttf", int(TILE_HEIGHT * 0.75) )
    #font = ImageFont.truetype("/nix/store/55yiv81sdqyvzjc1y975ql4ag8n1ij5n-
    #dejavu-fonts-2.37/share/fonts/truetype/DejaVuSans.ttf", 400)

    img = Image.new('RGB', (TILE_WIDTH ,TILE_HEIGHT), (255,255,255))

    draw = ImageDraw.Draw(img)
    draw.text((TILE_WIDTH / 2 , TILE_HEIGHT / 2 ), "", (255,255,255), font=font, anchor="mm")

    img.save("number-images/empty.gif")
    images.append("number-images/empty.gif")

    for i in range(1, nbr):
        img = Image.new('RGB', (TILE_WIDTH ,TILE_HEIGHT), TILE_BACKGROUND_COLOR)

        draw = ImageDraw.Draw(img)
        draw.text((TILE_WIDTH / 2 , TILE_HEIGHT / 2 ), str(i), TILE_FONT_COLOR, font=font, anchor="mm")

        img.save("number-images/{}.gif".format(i))
        images.append("number-images/{}.gif".format(i))

def register_images():
    global screen
    for i in range(len(images)):
        screen.addshape(images[i])

def index_2d(my_list, v):
    """Returns the position of an element in a 2D list."""
    for i, x in enumerate(my_list):
        if v in x:
            return (i, x.index(v))

def swap_tile(tile):
    """Swaps the position of the clicked tile with the empty tile."""
    global screen

    current_i, current_j = index_2d(board, tile)
    empty_i, empty_j = find_empty_square_pos()
    empty_square = board[empty_i][empty_j]

    if is_adjacent([current_i, current_j], [empty_i, empty_j]):
        temp = board[empty_i][empty_j]
        board[empty_i][empty_j] = tile
        board[current_i][current_j] = temp

        draw_board()

def is_adjacent(el1, el2):
    """Determines whether two elements in a 2D array are adjacent."""
    if abs(el2[1] - el1[1]) == 1 and abs(el2[0] - el1[0]) == 0:
        return True
    if abs(el2[0] - el1[0]) == 1 and abs(el2[1] - el1[1]) == 0:
        return True
    return False

def find_empty_square_pos():
    """Returns the position of the empty square."""
    global board
    for row in board:
        for candidate in row:
            if candidate.shape() == "number-images/empty.gif":
                empty_square = candidate

    return index_2d(board, empty_square)

def move_tile(direction):
    global board, screen

    turtle.speed(0)

    empty_i, empty_j = find_empty_square_pos()
    directions = ["up", "down", "left", "right"]

    if empty_i == 0 and direction == Directions.UP: 
        raise Exception ("Incorrect Direction \"UP\" !")

    if empty_i >= NUM_ROWS - 1 and direction == Directions.DOWN:
        raise Exception ("Incorrect Direction \"DOWN\" !")

    if empty_j == 0 and direction == Directions.LEFT:
        raise Exception ("Incorrect Direction \"LEFT\" !")
        
    if empty_j >= NUM_COLS - 1 and direction == Directions.RIGHT:
        raise Exception ("Incorrect Direction \"RIGHT\" !")

    if direction == Directions.UP: swap_tile(board[empty_i - 1][empty_j])
    if direction == Directions.DOWN: swap_tile(board[empty_i + 1][empty_j])
    if direction == Directions.LEFT: swap_tile(board[empty_i][empty_j - 1])
    if direction == Directions.RIGHT: swap_tile(board[empty_i][empty_j + 1])

def draw_board():
    global screen, board

    for i in range(NUM_ROWS):
        for j in range(NUM_COLS):
            tile = board[i][j]
            tile.showturtle()
            tile.goto(-( + ( (TILE_WIDTH + 2) * NUM_COLS ) / 2) + TILE_WIDTH / 2 + j * (TILE_WIDTH + 2),
                    (  ( (TILE_WIDTH + 2)  * NUM_ROWS) / 2 ) - TILE_HEIGHT / 2 - i * (TILE_HEIGHT + 2))

def create_tiles_from_matrix():

    board = [["#" for _ in range(NUM_COLS)] for _ in range(NUM_ROWS)]

    for i in range(NUM_ROWS):
        for j in range(NUM_COLS):
            tile_num = int(matrix[NUM_COLS * i + j])

            if (tile_num == max(map(int, matrix))):
                tile_num = 0

            tile = turtle.Turtle(images[tile_num])
            tile.penup()
            board[i][j] = tile

            def click_callback(x, y, tile=tile):
                """Passes `tile` to `swap_tile()` function."""
                return swap_tile(tile)

            tile.onclick(click_callback)
    return board

def create_buttons_tkinter():
    """An alternative approach to creating a button using Tkinter."""
    global screen
    canvas = screen.getcanvas()
    button = tk.Button(canvas.master, text="Scramble", background="cadetblue", foreground="white", bd=0)
    canvas.create_window(0, -240, window=button)

# TODO Replay Button
# TODO Next move Button
# TODO Auto play
# TODO Pause
# TODO Back

