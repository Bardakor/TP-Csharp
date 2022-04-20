#!/usr/bin/env python3

from PIL import Image
from PIL import ImageDraw
from PIL import ImageFont
from cgitb import handler
from reader import Directions, matrix, steps, tmp_steps
from typing import Dict

import math
import random
import tkinter as tk
import tkinter.ttk as ttk
import turtle



LEN_STEPS = len(steps)

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
WINDOW_HEIGHT = TILE_HEIGHT * NUM_ROWS + NUM_ROWS;
WINDOW_WIDTH = TILE_WIDTH * NUM_COLS + NUM_ROWS;

images = []

def create_images(nbr):
    """
        Create image from 1 to nbr
    """
    font = ImageFont.truetype("Viewer/fonts/OpenSans-Regular.ttf", int(TILE_HEIGHT * 0.75) )

    img = Image.new('RGB', (TILE_WIDTH ,TILE_HEIGHT), (240,248,255))

    draw = ImageDraw.Draw(img)
    draw.text((TILE_WIDTH / 2 , TILE_HEIGHT / 2 ), "", (255,255,255), font=font, anchor="mm")

    img.save("Viewer/number-images/empty.gif")
    images.append("Viewer/number-images/empty.gif")

    for i in range(1, nbr):
        img = Image.new('RGB', (TILE_WIDTH ,TILE_HEIGHT), TILE_BACKGROUND_COLOR)

        draw = ImageDraw.Draw(img)
        draw.text((TILE_WIDTH / 2 , TILE_HEIGHT / 2 ), str(i), TILE_FONT_COLOR, font=font, anchor="mm")

        img.save("Viewer/number-images/{}.gif".format(i))
        images.append("Viewer/number-images/{}.gif".format(i))

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
            if candidate.shape() == "Viewer/number-images/empty.gif":
                empty_square = candidate

    return index_2d(board, empty_square)

def move_tile(direction):
    global board, screen

    empty_i, empty_j = find_empty_square_pos()

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

def next_move():
    if (len(steps) < 2):
        button_next['state'] = 'disabled'

    if (steps == []):
        return

    button_prev['state'] = 'normal'
    screen.tracer(0)
    tmp_steps.insert(0, steps.pop(0))
    progress(1)
    move_tile(tmp_steps[0])
    screen.tracer(1)

def reverse_direction(direction):
    lookuptable = [(Directions.UP, Directions.DOWN), (Directions.LEFT, Directions.RIGHT)]

    reverse = [(dir,rev) for (dir,rev) in lookuptable if dir == direction or rev == direction][0]

    # Get the reverse
    return reverse[reverse[0] == direction]

def prev_move():
    if (len(tmp_steps) < 2):
        button_prev['state'] = 'disabled'

    if (tmp_steps == []):
        return

    button_next['state'] = 'normal'
    screen.tracer(0)
    progress(-1)

    steps.insert(0,tmp_steps.pop(0))

    # Get the reverse
    move_tile(reverse_direction(steps[0]))
    screen.tracer(1)

def retrieve_steps():
    screen.tracer(0)
    for step in reversed(steps):
        move_tile(reverse_direction(step))

    button_play['background'] = "green"
    button_play['text'] = "Play !"
    button_play['command'] = follow_steps

    button_play['state'] = 'normal'
    button_next['state'] = 'normal'
    button_prev['state'] = 'normal'

    screen.onkey(next_move, "Right")
    screen.onkey(prev_move, "Left")
    screen.onkey(follow_steps, "space")

    progress_bar['value'] = 0
    screen.tracer(1)

def draw_board():
    global screen, board

    for i in range(NUM_ROWS):
        for j in range(NUM_COLS):
            tile = board[i][j]
            tile.showturtle()
            tile.goto(-( + ( (TILE_WIDTH + 2) * NUM_COLS ) / 2) + TILE_WIDTH / 2 + j * (TILE_WIDTH + 2),
                    (  ( (TILE_WIDTH + 2)  * NUM_ROWS) / 2 ) - TILE_HEIGHT / 2 - i * (TILE_HEIGHT + 2) + 50)

def create_tiles_from_matrix():
    turtle.speed(10)

    board = [["#" for _ in range(NUM_COLS)] for _ in range(NUM_ROWS)]

    for i in range(NUM_ROWS):
        for j in range(NUM_COLS):
            tile_num = int(matrix[NUM_COLS * i + j])

            # if (tile_num == max(map(int, matrix))):
            #     tile_num = 0

            tile = turtle.Turtle(images[tile_num])
            tile.penup()
            board[i][j] = tile

    return board

def create_buttons_tkinter():
    """An alternative approach to creating a button using Tkinter."""
    global screen, button_play, button_next, button_prev, progress_bar

    canvas = screen.getcanvas()

    screen.onkey(next_move, "Right")
    button_next = tk.Button(canvas.master, text="Next", background="cadetblue", foreground="white", bd=0, command=next_move)
    canvas.create_window(( (TILE_WIDTH + 2) * NUM_COLS ) / 2, +230, window=button_next)

    screen.onkey(prev_move, "Left")
    button_prev = tk.Button(canvas.master, text="Prev", background="cadetblue", foreground="white", bd=0, command=prev_move)
    canvas.create_window( - ((TILE_WIDTH + 2) * NUM_COLS ) / 2, +230, window=button_prev)


    screen.onkey(follow_steps, "space")
    button_play = tk.Button(canvas.master, text="Play !", background="green", foreground="white", bd=0, command=follow_steps, activebackground='lawngreen')
    canvas.create_window( 0, +230, window=button_play)

    progress_bar = ttk.Progressbar(canvas.master, orient='horizontal', length= ((TILE_WIDTH + 2) * NUM_COLS ), mode='determinate', maximum=LEN_STEPS)

    canvas.create_window(0, +180, window=progress_bar)

    screen.listen()

def progress(incr):
    if progress_bar['value'] == LEN_STEPS  and incr > 0:
        return
    progress_bar['value'] += incr

def follow_steps():

    screen.onkey(None, "Left")
    screen.onkey(None, "space")
    screen.onkey(None, "Right")
    button_next['state'] = 'disabled'
    button_prev['state'] = 'disabled'

    button_play['state'] = 'disabled'
    button_play['text'] = "Process"
    button_play['background'] = "red"

    if steps == []:
        progress_bar['value'] = 0
        while tmp_steps !=[] :
            steps.insert(0, tmp_steps.pop(0))

    for step in steps:
        progress(1)
        move_tile(step)

    button_play['background'] = "blue"
    button_play['state'] = 'normal'
    button_play['text'] = "Reset !"
    button_play['activebackground'] = 'DodgerBlue2'

    screen.onkey(retrieve_steps, "space")
    button_play['command'] = retrieve_steps

def main():
    global screen, board

    if (math.sqrt(NBR_TILES) % 1):
        raise Exception("Number of tiles incorrect ! It must be a perfect square !")

    create_images(NBR_TILES)

    # Screen setup
    screen = turtle.Screen()
    turtle.hideturtle()

    screen.setup(WINDOW_WIDTH * 1.5,  WINDOW_HEIGHT * 1.5)

    screen.title("Sherlock Puzzle")

    screen.bgcolor("aliceblue")

    register_images()

    board = create_tiles_from_matrix()

    create_buttons_tkinter()

    screen.tracer(1)
    draw_board()

main()
turtle.done()