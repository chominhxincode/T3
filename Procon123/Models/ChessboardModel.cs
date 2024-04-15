using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Procon123.Models
{
    public class ChessboardModel
    {
        // Khai báo một lớp Worker để lưu trạng thái và vị trí của Worker
        public class Worker
        {
            public int Row { get; set; }
            public int Column { get; set; }
        }

        public int Rows { get; set; }
        public int Columns { get; set; }
        public string[,] Squares { get; set; } // Mảng 2 chiều đại diện cho các ô trên bàn cờ
        public List<Worker> Workers { get; set; } // Danh sách Worker trên bàn cờ

        // Các loại khu vực
        public enum AreaType { Neutral, Territory, Castle, Wall, Pond }
        public AreaType[,] Areas { get; set; }

        public ChessboardModel(int rows, int columns)
        {
            Rows = rows;
            Columns = columns;
            Squares = new string[Rows, Columns];
            Areas = new AreaType[Rows, Columns];
            Workers = new List<Worker>(); // Khởi tạo danh sách Worker

            // Khởi tạo giá trị ban đầu cho các loại khu vực (Trung lập)
            for (int i = 0; i < Rows; i++)
            {
                for (int j = 0; j < Columns; j++)
                {
                    Areas[i, j] = AreaType.Neutral;
                }
            }

            // Thiết lập các khu vực đặc biệt như Lâu đài và Ao
            SetInitialSpecialAreas();
        }

        // Cập nhật loại khu vực cho một ô cụ thể
        public void UpdateAreaType(int row, int column, AreaType areaType)
        {
            if (row >= 0 && row < Rows && column >= 0 && column < Columns)
            {
                Areas[row, column] = areaType;
            }
        }

        // Thiết lập các khu vực đặc biệt như Lâu đài và Ao
        private void SetInitialSpecialAreas()
        {
            Random rand = new Random();
            int castleCount = rand.Next(2, 6); // Số lượng Lâu đài ramdom từ 2 đến 5

            // Lặp qua các ô trên bàn cờ để đặt Lâu đài và Ao
            for (int i = 0; i < castleCount; i++)
            {
                int row = rand.Next(1, Rows - 1); // Tránh việc đặt ở hàng đầu hoặc hàng cuối
                int column = rand.Next(1, Columns - 1); // Tránh việc đặt ở cột đầu tiên hoặc cột cuối cùng

                // Đặt Lâu đài
                Areas[row, column] = AreaType.Castle;

                // Đặt Ao (khu vực xung quanh Lâu đài)
                for (int r = row - 1; r <= row + 1; r++)
                {
                    for (int c = column - 1; c <= column + 1; c++)
                    {
                        if (r >= 0 && r < Rows && c >= 0 && c < Columns && !(r == row && c == column))
                        {
                            Areas[r, c] = AreaType.Pond;
                        }
                    }
                }
            }
        }

        // Phương thức để thực hiện các chuyển động của thợ thủ công
        public void PerformMove(int currentRow, int currentColumn, int newRow, int newColumn)
        {
            // Kiểm tra xem vị trí mới có nằm trong phạm vi bàn cờ không
            if (newRow < 0 || newRow >= Rows || newColumn < 0 || newColumn >= Columns)
            {
                // Nếu vị trí mới nằm ngoài phạm vi bàn cờ, không thực hiện chuyển động
                return;
            }

            // Kiểm tra xem vị trí mới có phải là vị trí của Bức tường hoặc Ao không
            if (Areas[newRow, newColumn] == AreaType.Wall || Areas[newRow, newColumn] == AreaType.Pond)
            {
                // Nếu vị trí mới là Bức tường hoặc Ao, không thực hiện chuyển động
                return;
            }

            // Kiểm tra xem vị trí mới có trùng với vị trí của thợ thủ công khác không
            foreach (var worker in Workers)
            {
                // Bỏ qua vị trí của chính thợ thủ công hiện tại
                if (worker.Row == currentRow && worker.Column == currentColumn)
                {
                    continue;
                }

                // Kiểm tra xem vị trí mới có trùng với vị trí của thợ thủ công khác không
                if (worker.Row == newRow && worker.Column == newColumn)
                {
                    // Nếu trùng, không thực hiện chuyển động
                    return;
                }
            }

            // Nếu điều kiện trên đều không áp dụng, thực hiện chuyển động bằng cách cập nhật vị trí của thợ thủ công
            Workers.First(w => w.Row == currentRow && w.Column == currentColumn).Row = newRow;
            Workers.First(w => w.Row == currentRow && w.Column == currentColumn).Column = newColumn;
        }
        // Thêm phương thức để cập nhật vị trí của Worker
        public void UpdateWorkerPosition(int currentRow, int currentColumn)
        {
            // Tìm Worker hiện tại trong danh sách Workers và cập nhật vị trí của nó
            // Trong trường hợp này, tôi giả định rằng chỉ có một Worker được di chuyển
            // và tôi không xác định vị trí mới của Worker trong yêu cầu, vì vậy tôi sẽ chỉ cập nhật vị trí hiện tại của Worker.
            // Bạn có thể điều chỉnh mã này để xác định vị trí mới của Worker và cập nhật nó trong mô hình.
            var worker = Workers.FirstOrDefault();
            if (worker != null)
            {
                worker.Row = currentRow;
                worker.Column = currentColumn;
            }
        }

    }

}

