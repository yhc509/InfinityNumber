# Infinity
거의 무한에 근접하는 수를 표현하는 구조체입니다.


## 구조
Infinity는 아래 두 데이터의 조합으로 표현됩니다.
- 자리수를 나타내는 int형 데이터
- 값을 나타내는 double형 데이터 (0 ~ 999.9999999.... 의 범위만 갖습니다.)

따라서 표현 가능한 수의 범위는
대략 **1k * 10^-2,147,483,648 ~ 1k*10^2,147,483,648** 정도가 됩니다.

## 표현
1000 단위로 자리수를 계산하여 단위를 붙입니다.
단위는 A,B,C,D, ... 알파벳 순으로 Z까지 할당되어 있으며, 단위를 더 추가할 수도 있습니다.
단위의 아래값으로는 소수점 한자리까지만 표현합니다.

- 1 => 1.0A
- 10 => 10.0A
- 1,000 => 1.0B
- 1,001 => 1.0B
- 1,000,000 => 1.0C

## 사용법

### 할당
아래는 모두 10.0A 를 표현하는 예시입니다.
```csharp
Infinity n = 10;
Infinity n = "10A";
Infinity n = "10.0A";
var n = new Infinity(10);
var n = new Infinity("10A");
var n = new Infinity("10.0A");
var n = new Infinity(10.0, "A");
var n = new Infinity(10.0, 0);
```

### 지원되는 사칙 연산
#### 더하기(+)
```csharp
Infinity n = 10;    // 10.0A
Infinity n2 = 100;  // 100.0A

n += 1;             // 11.0A
n += n2;            // 111.0A
```
- Infinity + int
- Infinity + long
- Infinity + float
- Infinity + double
- Infinity + Infinity

#### 빼기(-)
```csharp
Infinity n = 10;    // 10.0A
Infinity n2 = 100;  // 100.0A

n -= 10;            // 0.0A
n -= n2;            // -100.0A
```
- Infinity - int
- Infinity - long
- Infinity - float
- Infinity - double
- Infinity - Infinity

#### 곱하기(*)
```csharp
Infinity n = 10;    // 10.0A
Infinity n2 = 100;  // 100.0A

n *= 10;            // 100.0A
n *= (long) 10;     // 1.0B
n *= 2.5f;          // 2.5B
n *= 2.0;           // 5.0B
n *= n2;            // 500.0B
```
- Infinity * int
- Infinity * long
- Infinity * float
- Infinity * double
- Infinity * Infinity

#### 나누기(/)
```csharp
Infinity n = 1000;    // 10.0A
Infinity n2 = 10;  // 100.0A

n /= 10;            // 100.0A
n /= (long) 10;     // 10.0A
n /= 0.5f;          // 20.0A
n /= 0.5;           // 40.0A
n /= n2;            // 4.0A
```
- Infinity / int
- Infinity / long
- Infinity / float
- Infinity / double
- Infinity / Infinity

### 지원되는 비교 연산
#### ==, !=
- Infinity == Infinity
- Infinity != Infinity

#### >, <
- Infinity > Infinity
- Infinity < Infinity

#### Equals
- Infinity.Equals(Infinity)