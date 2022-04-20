type binTree =
  | Person of string * int
  | GenTree of binTree * binTree * binTree
  | Empty
;;

[@@@warning "-8"] (* avoiding warning that won't happen since file is always
                     valid (predicate) *)
(**
 * \brief Builds the tree corresponding to a valid .gen file
 * **)
let treeFromFile path =
  (* Utility, reads the first line of a file and splits it on '-' *)
  let read_line file =
  let in_chan = open_in file in
  let s = input_line in_chan in
    close_in in_chan;
    String.split_on_char '-' s
  in

  (* List of suspects on the line *)
  let lines = read_line path in

  (* Building the tree  using previously defined list *)
  let rec build x =
    if x >= List.length lines then
         Empty
    else
      let names = String.split_on_char ' ' (List.nth lines x) in
      let name::names = names in
      let suspicion::names = names in
      GenTree (Person (name, int_of_string suspicion),
               build (2 * x + 1),
               build (2 * x + 2))
  in
  build 0;
;;
[@@@warning "+8"]

let printTree tree = ()
;;

let changeName tree oldName newName = Empty
;;

let findPath t name = []
;;
