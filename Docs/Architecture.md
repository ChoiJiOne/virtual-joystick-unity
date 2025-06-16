# 코드 구조 및 책임 분리 설명

## 개요
- Virtual Joystick 시스템의 코드 구조와 각 컴포넌트의 책임 분리 설명

## 전체 구성
- Virtual Joystick 시스템은 다음 3개의 주요 컴포넌트로 구성
    - TouchInputDispatcher
    - VirtualJoystick
    - PlayerController
- 외부 시스템은 VirtualJoystick이 제공하는 입력 벡터 사용

## 컴포넌트 상세

### TouchInputDispatcher
- 역할
    - 터치 입력 이벤트 수집 및 분배
- 책임
    - `IPointerDownHandler`, `IDragHandler`, `IPointerUpHandler` 인터페이스 구현
    - 터치 시작, 이동, 종료 이벤트 감지
    - 터치 이벤트를 UnityAction으로 외부에 전달 (`OnBeginTouch`, `OnMoveTouch`, `OnEndTouch`)
    - 입출력 decoupling
- 특징
    - 입력 영역만 담당 → 입력 로직과 분리됨
    - 테스트 및 재사용이 용이한 구조

### VirtualJoystick
- 역할
    - 조이스틱 UI 및 입력 벡터 계산
- 책임
    - 입력 좌표를 캔버스 로컬 좌표계로 변환
    - 입력 반경 내에서 핸들 이동 제한 (클램핑 처리)
    - 입력 벡터 계산 및 정규화
    - 외부 시스템에서 Axis 프로퍼티를 통해 벡터 조회 가능
    - UI 핸들 활성/비활성 관리
- 이벤트 바인딩
    - `TouchInputDispatcher`의 델리게이트에 연결하여 입력 수신

### PlayerController
- 역할
    - 조이스틱 입력을 받아 실제 오브젝트 이동 처리
- 책임
    - VirtualJoystick의 `Axis` 값을 조회하여 Rigidbody 이동 속도 적용
    - 입력값을 3D 이동 벡터로 변환 (x, z축 이동)
    - 이동 속도 계수 적용 (`_moveSpeed`)
- 특징
    - 이동 로직과 입력 시스템 분리
    - VirtualJoystick이 어떻게 구현되었는지 알 필요 없음 → 인터페이스 분리의 효과

### 컴포넌트 관계도

```
[ Unity Touch Event ]
        ↓
[ TouchInputDispatcher ]
        ↓ (델리게이트)
[ VirtualJoystick ]
        ↓ (Axis)
[ PlayerController ]
```

## 책임 분리 요약

| 컴포넌트                 | 책임             |
| -------------------- | -------------- |
| TouchInputDispatcher | 터치 이벤트 수집 및 전달 |
| VirtualJoystick      | UI 기반 입력 벡터 산출 |
| PlayerController     | 물리 이동 처리       |

## 설계 의도
- 입력 시스템과 이동 시스템 분리 → 재사용 용이
- 이벤트 기반 아키텍처 → 느슨한 결합 유지