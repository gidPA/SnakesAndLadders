public static class Dice {
    public static int Roll(int rollAmount){
        int total = 0;
        Random random = new Random();
        for(int i = 0; i < rollAmount; i++){
            total += random.Next(1, 7);
        }
        return total;
    }
}