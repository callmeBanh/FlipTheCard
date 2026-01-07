# FlipTheCard - Tổng quan dự án

Một project Unity nhỏ (mini game) tên **FlipTheCard** - trò chơi lật thẻ/bộ nhớ đơn giản.

## Mục tiêu

- Game bài lật thẻ (matching / memory) dành cho thiết bị desktop/mobile, bao gồm UI, Scenes, và logic quản lý thẻ.

## Phiên bản Unity

- Dự án sử dụng phiên bản Unity theo file ProjectSettings/ProjectVersion.txt: `m_EditorVersion: 6000.3.2f1`.

# FlipTheCard — Tổng quan dự án

FlipTheCard là một mini game Unity (trò chơi lật thẻ / memory matching). README này mô tả nhanh mục tiêu, cấu trúc sản phẩm, các scene hiện có, hướng dẫn mở dự án và cách build.

## Phiên bản Unity

- Dự án hiện tại được cấu hình cho Unity: [ProjectSettings/ProjectVersion.txt](ProjectSettings/ProjectVersion.txt#L1-L2) (ví dụ: `m_EditorVersion: 6000.3.2f1`).

## Mục tiêu ngắn gọn

- Trò chơi lật thẻ: lật thẻ để tìm cặp giống nhau.
- Giao diện menu, lựa chọn level, màn chơi chính, màn kết quả.

## Scenes (có sẵn)

- `StartGame.unity` — màn khởi đầu / menu chính
- `ChooseLevel.unity` — màn chọn level
- `UserPlay.unity` — màn chơi chính (gameplay)
- `Result.unity` — màn kết quả tổng quát
- `Result_Fall.unity` — biến thể màn kết quả (rơi/hiệu ứng)

Các scene được lưu trong `Assets/Project/Scene`.

## Cấu trúc sản phẩm (Product Structure)

- `Assets/`
  - `Project/`
    - `Scene/` : Scene Unity (những file liệt kê ở trên)
    - `Scripts/` : Mã C# cho gameplay (controllers, managers, models)
    - `Prefabs/` : Prefab UI và object
    - `Audios/` : File âm thanh
    - `Sprites/` : Hình ảnh, icon, sheet
  - `Background/`, `TextMesh Pro/`, ... : Các asset hỗ trợ khác
- `ProjectSettings/` : Cấu hình Unity (phiên bản, input, graphics)
- `Packages/` : Danh sách packages (manifest)

Lưu ý: controls cụ thể (phím tắt, gesture) có thể nằm trong code; xem các component UI trong `Assets/Project/Prefabs`.

## Mở dự án (Quick start)

1. Mở Unity Hub.
2. Chọn `Add` và trỏ tới thư mục gốc dự án (thư mục chứa `Assets` và `ProjectSettings`).
3. Dùng phiên bản Unity khớp hoặc tương thích với `ProjectSettings/ProjectVersion.txt`.
4. Mở scene muốn thử nghiệm (ví dụ: `StartGame.unity` hoặc `UserPlay.unity`) và nhấn Play.

## Kiểm thử & Debug

- Dùng Play mode trong Unity Editor để kiểm thử nhanh.
- Sử dụng `Debug.Log` để in log; xem Console để debug.

## Quy trình phát triển

- Giữ code trong `Assets/Project/Scripts` sạch và tách biệt UI vs game logic.
- Thêm unit tests hoặc playmode tests nếu cần (Test Runner).

## Ghi chú khi thêm asset lớn

- Với audio/texture lớn, cân nhắc dùng Addressables hoặc đặt ngoài thư mục `Resources` để quản lý hiệu quả.

## Tệp quan trọng

- Phiên bản Unity: [ProjectSettings/ProjectVersion.txt](ProjectSettings/ProjectVersion.txt#L1-L2)
- Script chính: [Assets/Project/Scripts](Assets/Project/Scripts)
- Scenes: [Assets/Project/Scene](Assets/Project/Scene)
