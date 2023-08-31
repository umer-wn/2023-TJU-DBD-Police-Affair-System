--将salary从policemen转到employ中--
ALTER TABLE policemen
DROP COLUMN salary;

ALTER TABLE employ
ADD  salary varchar(20) DEFAULT ‘5000’;

--新建工资条属性，包含流水号、接受人警号、姓名、发放警局、发薪日、基本工资、补贴--
CREATE TABLE payroll_stub (
  payroll_number VARCHAR(20) PRIMARY KEY,
  police_number VARCHAR(20),
  police_name VARCHAR(20),
  STATION_ID VARCHAR(20), 
  pay_day DATE,
  salary VARCHAR(6),
  subsidy VARCHAR(6),
  FOREIGN KEY (STATION_ID) REFERENCES police_station(STATION_ID),
  FOREIGN KEY (police_number) REFERENCES policemen(police_number)
);
-- 将数据从旧表插入新表

INSERT INTO payroll_stub (payroll_number, police_number, police_name, station_ID, pay_day,salary,subsidy)VALUES(1, '3889043', '李秀英', '130101005', '1-1月 -23','5000','100');
INSERT INTO payroll_stub  (payroll_number, police_number, police_name, station_ID, pay_day,salary,subsidy) VALUES(2, '3889043', '李秀英', '130101005', '1-2月 -98','5000','100');
INSERT INTO payroll_stub  (payroll_number, police_number, police_name, station_ID, pay_day,salary,subsidy) VALUES(3, '9836582', '张伟', '120102004', '1-6月 -23','5000','100');
INSERT INTO payroll_stub  (payroll_number, police_number, police_name, station_ID, pay_day,salary,subsidy) VALUES(4, '9836582', '张伟', '120102004', '21-7月 -08','5000','100');
INSERT INTO payroll_stub  (payroll_number, police_number, police_name, station_ID, pay_day,salary,subsidy) VALUES(5, '1691715', '刘娜', '110102002', '05-3月 -23','5000','100');
INSERT INTO payroll_stub  (payroll_number, police_number, police_name, station_ID, pay_day,salary,subsidy) VALUES(6, '1691715', '刘娜', '110102002', '29-4月 -03','5000','100');
INSERT INTO payroll_stub  (payroll_number, police_number, police_name, station_ID, pay_day,salary,subsidy) VALUES(7, '0975704', '陈敏', '110101001', '18-8月 -22','5000','100');
INSERT INTO payroll_stub  (payroll_number, police_number, police_name, station_ID, pay_day,salary,subsidy) VALUES(8, '0975704', '陈敏', '110101001', '03-10月-02','5000','100');
INSERT INTO payroll_stub  (payroll_number, police_number, police_name, station_ID, pay_day,salary,subsidy) VALUES(9, '0715805', '刘洋', '320101006', '1-7月 -22','5000','100');
INSERT INTO payroll_stub  (payroll_number, police_number, police_name, station_ID, pay_day,salary,subsidy) VALUES(10, '0715805', '刘洋', '320101006', '13-8月 -12','5000','100');
INSERT INTO payroll_stub  (payroll_number, police_number, police_name, station_ID, pay_day,salary,subsidy) VALUES(11, '4264114', '王艳', '440102007', '1-11月-22','5000','100');
INSERT INTO payroll_stub  (payroll_number, police_number, police_name, station_ID, pay_day,salary,subsidy) VALUES(12, '4264114', '王艳', '440102007', '29-5月 -10','5000','100');
INSERT INTO payroll_stub  (payroll_number, police_number, police_name, station_ID, pay_day,salary,subsidy) VALUES(13, '8837988', '张静', '510101008', '21-2月 -23','5000','100');
INSERT INTO payroll_stub  (payroll_number, police_number, police_name, station_ID, pay_day,salary,subsidy) VALUES(14, '8837988', '张静', '510101008', '17-12月-90','5000','100');
INSERT INTO payroll_stub  (payroll_number, police_number, police_name, station_ID, pay_day,salary,subsidy) VALUES(15, '6827713', '李勇', '320102009', '17-4月 -23','5000','100');
INSERT INTO payroll_stub  (payroll_number, police_number, police_name, station_ID, pay_day,salary,subsidy) VALUES(16, '6827713', '李勇', '320102009', '23-2月 -01','5000','100');
INSERT INTO payroll_stub  (payroll_number, police_number, police_name, station_ID, pay_day,salary,subsidy) VALUES(17, '4348435', '刘杰', '330101010', '23-9月 -22','5000','100');
INSERT INTO payroll_stub  (payroll_number, police_number, police_name, station_ID, pay_day,salary,subsidy) VALUES(18, '4348435', '刘杰', '330101010', '14-6月 -06','5000','100');
INSERT INTO payroll_stub  (payroll_number, police_number, police_name, station_ID, pay_day,salary,subsidy) VALUES(19, '9234086', '刘燕', '610101011', '08-5月 -23','5000','100');
INSERT INTO payroll_stub  (payroll_number, police_number, police_name, station_ID, pay_day,salary,subsidy) VALUES(20, '9234086', '刘燕', '610101011', '27-3月 -95','5000','100');
INSERT INTO payroll_stub  (payroll_number, police_number, police_name, station_ID, pay_day,salary,subsidy) VALUES(21, '8268264', '张敏', '330102012', '08-12月-22','5000','100');
INSERT INTO payroll_stub  (payroll_number, police_number, police_name, station_ID, pay_day,salary,subsidy) VALUES(22, '8268264', '张敏', '330102012', '30-9月 -02','5000','100');
INSERT INTO payroll_stub  (payroll_number, police_number, police_name, station_ID, pay_day,salary,subsidy) VALUES(23, '9173384', '王勇', '440101013', '01-7月 -23','5000','100');
INSERT INTO payroll_stub  (payroll_number, police_number, police_name, station_ID, pay_day,salary,subsidy) VALUES(24, '9173384', '王勇', '440101013', '08-5月 -91','5000','100');

INSERT INTO payroll_stub  (payroll_number, police_number, police_name, station_ID, pay_day,salary,subsidy) VALUES(25, '5218977', '刘婷', '330103015', '03-1月 -23','5000','100');
INSERT INTO payroll_stub  (payroll_number, police_number, police_name, station_ID, pay_day,salary,subsidy) VALUES(26, '5218977', '刘婷', '330103015', '02-4月 -96','5000','100');
INSERT INTO payroll_stub  (payroll_number, police_number, police_name, station_ID, pay_day,salary,subsidy) VALUES(27, '7614546', '王静', '440103016', '06-2月 -23','5000','100');
INSERT INTO payroll_stub  (payroll_number, police_number, police_name, station_ID, pay_day,salary,subsidy) VALUES(28, '7614546', '王静', '440103016', '21-8月 -98','5000','100');
INSERT INTO payroll_stub  (payroll_number, police_number, police_name, station_ID, pay_day,salary,subsidy) VALUES(29, '4789420', '张瑶', '510102017', '19-5月 -23','5000','100');
INSERT INTO payroll_stub  (payroll_number, police_number, police_name, station_ID, pay_day,salary,subsidy) VALUES(30, '4789420', '张瑶', '510102017', '10-6月 -86','5000','100');
INSERT INTO payroll_stub  (payroll_number, police_number, police_name, station_ID, pay_day,salary,subsidy) VALUES(31, '4693712', '赵军', '610102018', '27-3月 -23','5000','100');
INSERT INTO payroll_stub  (payroll_number, police_number, police_name, station_ID, pay_day,salary,subsidy) VALUES(32, '4693712', '赵军', '610102018', '25-10月-93','5000','100');
INSERT INTO payroll_stub  (payroll_number, police_number, police_name, station_ID, pay_day,salary,subsidy) VALUES(33, '2085174', '张伟', '440105026', '29-6月 -23','5000','100');
INSERT INTO payroll_stub  (payroll_number, police_number, police_name, station_ID, pay_day,salary,subsidy) VALUES(34, '8712011', '李敏', '130101005', '11-9月 -95','5000','100');
INSERT INTO payroll_stub  (payroll_number, police_number, police_name, station_ID, pay_day,salary,subsidy) VALUES(35, '6916120', '刘静', '330102012', '19-10月-22','5000','100');
INSERT INTO payroll_stub  (payroll_number, police_number, police_name, station_ID, pay_day,salary,subsidy) VALUES(36, '6916120', '刘静', '120102004', '03-4月 -98','5000','100');
INSERT INTO payroll_stub  (payroll_number, police_number, police_name, station_ID, pay_day,salary,subsidy) VALUES(37, '9231816', '陈勇', '110102002', '14-2月 -23','5000','100');
INSERT INTO payroll_stub  (payroll_number, police_number, police_name, station_ID, pay_day,salary,subsidy) VALUES(38, '9231816', '陈勇', '110102002', '19-5月 -04','5000','100');
INSERT INTO payroll_stub  (payroll_number, police_number, police_name, station_ID, pay_day,salary,subsidy) VALUES(39, '0956317', '王霞', '440101013', '17-6月 -23','5000','100');
INSERT INTO payroll_stub  (payroll_number, police_number, police_name, station_ID, pay_day,salary,subsidy) VALUES(40, '0956317', '王霞', '110101001', '30-10月-01','5000','100');
INSERT INTO payroll_stub  (payroll_number, police_number, police_name, station_ID, pay_day,salary,subsidy) VALUES(41, '8988132', '刘平', '320103014', '16-7月 -22','5000','100');
INSERT INTO payroll_stub  (payroll_number, police_number, police_name, station_ID, pay_day,salary,subsidy) VALUES(42, '5480803', '杨丽', '330105021', '04-9月 -22','5000','100');
INSERT INTO payroll_stub  (payroll_number, police_number, police_name, station_ID, pay_day,salary,subsidy) VALUES(43, '1301569', '张勇', '610104028', '18-1月 -23','5000','100');
INSERT INTO payroll_stub  (payroll_number, police_number, police_name, station_ID, pay_day,salary,subsidy) VALUES(44, '0763368', '陈娟', '320104019', '30-5月 -23','5000','100');
INSERT INTO payroll_stub  (payroll_number, police_number, police_name, station_ID, pay_day,salary,subsidy) VALUES(45, '6586238', '张伟', '330104020', '11-12月-22','5000','100');

INSERT INTO payroll_stub  (payroll_number, police_number, police_name, station_ID, pay_day,salary,subsidy) VALUES(46, '3468353', '李敏', '440103016', '03-7月 -23','5000','100');
INSERT INTO payroll_stub  (payroll_number, police_number, police_name, station_ID, pay_day,salary,subsidy) VALUES(47, '0843435', '刘静', '510101008', '23-7月 -22','5000','100');
INSERT INTO payroll_stub  (payroll_number, police_number, police_name, station_ID, pay_day,salary,subsidy) VALUES(48, '8734165', '陈洁', '320104019', '03-10月-22','5000','100');
INSERT INTO payroll_stub  (payroll_number, police_number, police_name, station_ID, pay_day,salary,subsidy) VALUES(49, '8734165', '陈洁', '320104019', '22-9月 -87','5000','100');
INSERT INTO payroll_stub  (payroll_number, police_number, police_name, station_ID, pay_day,salary,subsidy) VALUES(50, '0664786', '张勇', '330104020', '08-4月 -23','5000','100');
INSERT INTO payroll_stub  (payroll_number, police_number, police_name, station_ID, pay_day,salary,subsidy) VALUES(51, '0664786', '张勇', '330104020', '15-1月 -90','5000','100');
INSERT INTO payroll_stub  (payroll_number, police_number, police_name, station_ID, pay_day,salary,subsidy) VALUES(52, '9884669', '李丽', '330105021', '15-6月 -23','5000','100');
INSERT INTO payroll_stub  (payroll_number, police_number, police_name, station_ID, pay_day,salary,subsidy) VALUES(53, '9884669', '李丽', '330105021', '18-11月-01','5000','100');
INSERT INTO payroll_stub  (payroll_number, police_number, police_name, station_ID, pay_day,salary,subsidy) VALUES(54, '7104084', '王勇', '440104022', '14-7月 -22','5000','100');
INSERT INTO payroll_stub  (payroll_number, police_number, police_name, station_ID, pay_day,salary,subsidy) VALUES(55, '7104084', '王勇', '440104022', '05-7月 -04','5000','100');
INSERT INTO payroll_stub  (payroll_number, police_number, police_name, station_ID, pay_day,salary,subsidy) VALUES(56, '3242355', '陈娟', '510103023', '08-9月 -22','5000','100');

INSERT INTO payroll_stub  (payroll_number, police_number, police_name, station_ID, pay_day,salary,subsidy) VALUES(57, '3242355', '陈娟', '510103023', '08-3月 -99','5000','100');
INSERT INTO payroll_stub  (payroll_number, police_number, police_name, station_ID, pay_day,salary,subsidy) VALUES(58, '2402834', '张伟', '610103024', '20-7月 -22','5000','100');
INSERT INTO payroll_stub  (payroll_number, police_number, police_name, station_ID, pay_day,salary,subsidy) VALUES(59, '2402834', '张伟', '610103024', '19-5月 -94','5000','100');
INSERT INTO payroll_stub  (payroll_number, police_number, police_name, station_ID, pay_day,salary,subsidy) VALUES(60, '5639807', '李敏', '320105025', '31-1月 -23','5000','100');
INSERT INTO payroll_stub  (payroll_number, police_number, police_name, station_ID, pay_day,salary,subsidy) VALUES(61, '5639807', '李敏', '320105025', '28-4月 -85','5000','100');
INSERT INTO payroll_stub  (payroll_number, police_number, police_name, station_ID, pay_day,salary,subsidy) VALUES(62, '8167027', '刘静', '440105026', '25-2月 -23','5000','100');

INSERT INTO payroll_stub  (payroll_number, police_number, police_name, station_ID, pay_day,salary,subsidy) VALUES(63, '8167027', '刘静', '440105026', '13-10月-01','5000','100');
INSERT INTO payroll_stub  (payroll_number, police_number, police_name, station_ID, pay_day,salary,subsidy) VALUES(64, '7233141', '陈勇', '510104027', '05-6月 -23','5000','100');
INSERT INTO payroll_stub  (payroll_number, police_number, police_name, station_ID, pay_day,salary,subsidy) VALUES(65, '7233141', '陈勇', '510104027', '16-6月 -03','5000','100');
INSERT INTO payroll_stub  (payroll_number, police_number, police_name, station_ID, pay_day,salary,subsidy) VALUES(66, '7003217', '张勇', '320106029', '28-8月 -22','5000','100');
INSERT INTO payroll_stub  (payroll_number, police_number, police_name, station_ID, pay_day,salary,subsidy) VALUES(67, '7003217', '张勇', '320106029', '02-7月 -88','5000','100');
INSERT INTO payroll_stub  (payroll_number, police_number, police_name, station_ID, pay_day,salary,subsidy) VALUES(68, '1313149', '陈娟', '330106030', '25-5月 -23','5000','100');
INSERT INTO payroll_stub  (payroll_number, police_number, police_name, station_ID, pay_day,salary,subsidy) VALUES(69, '1313149', '陈娟', '330106030', '23-2月 -99','5000','100');