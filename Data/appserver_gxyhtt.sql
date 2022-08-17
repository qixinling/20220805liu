/*
Navicat MySQL Data Transfer

Source Server         : appserver
Source Server Version : 50734
Source Host           : 43.129.174.178:3306
Source Database       : appserver_gxyhtt

Target Server Type    : MYSQL
Target Server Version : 50734
File Encoding         : 65001

Date: 2021-08-26 12:33:46
*/

SET FOREIGN_KEY_CHECKS=0;

-- ----------------------------
-- Table structure for db_article
-- ----------------------------
DROP TABLE IF EXISTS `db_article`;
CREATE TABLE `db_article` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `articletitle` longtext NOT NULL,
  `articlecontent` longtext NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=10 DEFAULT CHARSET=utf8mb4;

-- ----------------------------
-- Records of db_article
-- ----------------------------
INSERT INTO `db_article` VALUES ('1', '公司简介', '这是公司简介1<p>9</p>');
INSERT INTO `db_article` VALUES ('2', '会员帮助', '这是会员帮助<p><br></p>');
INSERT INTO `db_article` VALUES ('3', '充值帮助', '<p><img src=\"http://liu20200620.gxzzwl.com/Upload/admin9079c317-4f3a-4219-b384-3d7cb0a1378e.jpg\" alt=\"\">温馨提示<br></p><p>请在汇款后拍照并上传支付凭证</p>22');
INSERT INTO `db_article` VALUES ('4', '提现帮助', '<p>温馨提示</p><p>到账时间:24小时。</p>');
INSERT INTO `db_article` VALUES ('5', '转账帮助', '<p>温馨提示</p><p>转账前请确认收款人是否正确</p>');
INSERT INTO `db_article` VALUES ('6', '转换帮助', '<p><span style=\"background-color: rgb(255, 255, 255);\">这是转换帮助</span></p>');
INSERT INTO `db_article` VALUES ('7', 'test', '<p>渠道商1</p>');
INSERT INTO `db_article` VALUES ('8', 'test', '<p>感谢您申请商标圈账户！在成为商标圈用户之前，请您仔细阅读以下条文，确保您理解并同意本协议全部内容。商标圈平台是由杭州名商网络有限公司（名商网）运营的商标转让的综合平台，域名为shangbiao.com。以下内容将构成您和杭州名商网络有限公司之间的服务合同条款。</p><h4>一、用户注册</h4><p>您同意本协议并注册成功即成为商标圈的注册用户直至您的账户注销。商标圈可能在必要的时候对本协议条款及商标圈各单项服务协议进行更改，此种更改在商标圈上公布或在具体服务过程中经双方以口头、书面等形式协商一致生效。您可以选择停止使用商标圈相关的服务或者注销您在商标圈的账户，否则商标圈将认为您同意更改后的服务条款。未在商标圈发布或在具体服务过程中未经商标圈告知的服务条款将不具有法律效力，除非签有书面协议，并且仅对签署协议的当事人有效。</p><h4>二、用户账户</h4><p>您应当拥有中华人民共和国法律认可的完全民事权利能力和完全民事行为能力，否则您和能够代表您行使权利或承担责任的其他主体将承担一切后果。为此，您应当提供相应的证明。如果您是自然人，此类证明可以是您的居民身份证件、个人户营业执照。如果您是法人，此类证明可以是您的营业执照。如果您是其他组织类型，您应当提供相应的合法证明。</p><p>如果使用商标圈提供的各单项服务，您可能还需要提交其他相关的资料和信息。为了提供更好的服务，您也可以向我们提供其他资料和信息，我们将按照商标圈的隐私政策保护您的资料和信息。</p><p>您应当保护好您的账户，除非已经得到您的提前通知，商标圈将认为您的账户处于您的控制之下。如果您的账户在不受您控制的情况下处于风险状态或者已经发生损失，请您及时以有效方式通知商标圈，您可以要求商标圈暂停服务或者冻结账户。</p><p>如果您使用商标圈账户直接访问其他网站，您应当遵守该网站的服务条款。</p><h4>三、账户管理</h4><p>1、商标圈帐号的所有权归商标圈所有，用户完成申请注册手续后，获得商标圈帐号的使用权，该使用权仅属于初始申请注册人，禁止赠与、借用、租用、转让或售卖。商标圈因经营需要，有权回收用户的商标圈帐号。</p><p>2、用户可以更改、删除商标圈帐户上的个人资料、注册信息及传送内容等，但需注意，删除有关信息的同时也会删除用户储存在系统中的文字和图片。用户需承担该风险。</p><p>3、用户有责任妥善保管注册帐号信息及帐号密码的安全，因用户保管不善可能导致遭受盗号或密码失窃，责任由用户自行承担。用户需要对注册帐号以及密码下的行为承担法律责任。用户同意在任何情况下不使用其他用户的帐号或密码。在用户怀疑他人使用其帐号或密码时，用户同意立即通知商标圈。</p><p>4、用户应遵守本协议的各项条款，正确、适当地使用本服务，如因用户违反本协议中的任何条款，商标圈在通知用户后有权依据协议中断或终止对违约用户商标圈帐号提供服务。同时，商标圈保留在任何时候收回商标圈帐号、用户名的权利。</p><p>5、如用户注册商标圈帐号后一年不登录，通知用户后，商标圈可以收回该帐号，以免造成资源浪费，由此造成的不利后果由用户自行承担。</p><h4>四、用户责任</h4><p>用户使用商标圈必须遵守所有适用的国家法律、地方法规和国际准则。用户对用户账户进行的一切操作及言论负完全的责任，用户注销账户后，仍然应当对注销前的操作和言论负责。</p><p>用户必须遵循：</p><p>（1）从中国境内向外传输技术性资料时必须符合中国有关法规。</p><p>（2）使用网络服务不作非法用途</p><p>（3）不干扰或混乱网络服务</p><p>（4）遵守所有使用网络服务的网络协议、规定、程序和惯例。</p><p>用户须承诺不传输任何非法的、骚扰性的、中伤他人的、辱骂性的、恐吓性的、伤害性的、庸俗的，淫秽等信息资料。用户也不能传输任何教唆他人构成犯罪行为的资料。不得进行任何有可能对商标圈的系统造成任何不良的影响的操作，不能传输助长国内不利条件和涉及国家安全的资料。不能传输任何不符合当地法规、国家法律和国际准则的资料。未经许可而非法进入其它电脑系统是禁止的。若用户的行为不符合以上提到的服务条款，商标圈单方面有权冻结或注销用户账户。用户需对用户本身在网上的行为承担法律责任。用户若在商标圈上散布和传播反动、色情或其他违反国家法律的信息，商标圈的系统记录有可能作为用户违反法律的证据。</p><h4>五、数据储存</h4><p>1、商标圈不对用户在本服务中相关数据的删除或储存失败负责。</p><p>2、商标圈可以根据实际情况自行决定用户在本服务中数据的最长储存期限，并在服务器上为其分配数据最大存储空间等。用户可根据自己的需要自行备份本服务中的相关数据。</p><p>3、如用户停止使用本服务或本服务终止，商标圈可以从服务器上永久地删除用户的数据。本服务停止、终止后商标圈没有义务向用户返还任何数据。</p><h4>六、风险承担</h4><p>1、用户理解并同意，商标圈仅为用户提供信息分享、传送及获取的平台，用户必须为自己注册帐号下的一切行为负责，包括用户所传送的任何内容以及由此产生的任何后果。用户应对商标圈及本服务中的内容自行加以判断，并承担因使用内容而引起的所有风险，包括因对内容的正确性、完整性或实用性的依赖而产生的风险商标圈无法且不会对因用户行为而导致的任何损失或损害承担责任。</p><p>如果用户发现任何人违反本协议约定或以其他不当的方式使用本服务，请立即向商标圈举报或投诉，商标圈将依本协议约定进行处理。</p><p>2、用户理解并同意，因业务发展需要，商标圈保留单方面对本服务的全部或部分服务内容变更、暂停、终止或撤销的权利，用户需承担此风险。</p><h4>七、商标圈服务</h4><p>商标圈为用户提供商标交易、商标论坛等相关服务各项服务。用户应当按照商标圈的服务规则和页面提示进行操作，也可以直接联系商标圈获取更多的服务。商标圈各单项服务协议与本协议冲突或是有特别规定的，应当优先适用各单项服务协议。</p><p>商标圈的各项服务费用在商标圈上公布，商标圈有权根据有关政策的变化、市场情况、服务范围变化等原因，单方面调整价格标准，调整的项目将在商标圈上更新，不作额外的通知。商标圈有权拒绝为不按照商标圈要求提供信息或不服从商标圈服务规则的用户提供服务，有权注销或者冻结该用户账户。</p><p>用户有权取回用户在商标圈账户中未使用的资金，但不得利用商标圈资金账户套现信用卡资金，不得有其他违反银行、支付中介机构及商标圈资金规范的行为。用户在商标圈账户消费的的订单将提供明细以供查询的资金不产生任何形式的孳息。</p><p>商标圈的数据来源于互联网，版权归源网站所有，平台通过技术手段对信息进行汇总实现快照，无法避免后续网站内容的变更，汇总的数据如有出入本站不承担责任。</p><p>商标圈没有为用户保存账户各项资料和记录的义务。</p><h4>八、商标圈免责条款</h4><p>商标圈不保证提供的服务不受干扰、及时提供或免于出错。商标圈不保证所交易的商标具有适销性、适用性，也不能保证交易方实际拥有该商标。商标圈不对用户的真实身份进行核实。除非有效法律文书明确要求并提供相应保护，商标圈将遵守隐私政策，不对外提供用户信息。</p><p>因下列情形无法按照约定提供各项服务时，商标圈不承担责任，包括但不限于：</p><p>（1）商标圈公告的休假或者系统维护的。</p><p>（2）电信设备出现故障不能进行数据传输的。</p><p>（3）因台风、地震、海啸、洪水、停电、战争、恐怖袭击等不可抗力之因素，造成商标圈障碍不能提供服务的。</p><p>（4）由于黑客攻击、电信部门技术调整或故障、网站升级、银行方面的问题等原因而造成的服务中断或者延迟的。</p><h4>九、知识产权保护</h4><p>用户不得擅自在全球任何国家和地区申请与商标圈及其他服务的名称、标识、品牌、特色标志、域名相同或类似的商标、服务商标、域名、著作权等。用户不得擅自使用、复制、修改、改编、翻译、汇编、转载、发行商标圈所有内容，包括但不限于文字作品、图片作品、摄影作品、示意图、网站架构、网站画面的安排、网页设计。</p><h4>十、隐私政策</h4><p>商标圈隐私政策构成本协议的有效内容。隐私政策调整的，适用最新的隐私政策。</p><h4>十一、法律适用和管辖</h4><p>本协议及其他单项服务协议适用中华人民共和国法律，法律没有明文规定的，按照商标圈的服务和交易惯例解释。协议的部分条款发生无效的情形，不受影响的其他条款和协议仍然有效。因本协议及其他单项服务协议产生的争议，浙江省杭州市拱墅区人民法院为有权管辖法院。</p><h4>十二、其他</h4><p>工作日是指商标圈的营业日。除非商标圈公告通知休假，一般为中华人民共和国适用的休息日以及法定节假日之外的正常工作日。日期的计算使用公历年月日，时间的计算使用北京时间。</p><p>商标圈通过公告通知，公告的时间为送达到用户的时间；通过商标圈网页向用户提醒，提醒出现的时间为送达的时间；通过用户提供的联系方式向用户送达，用户或其代表签收、用户系统接收的时间为送达的时间。</p>');
INSERT INTO `db_article` VALUES ('9', 'test', '@p0');

-- ----------------------------
-- Table structure for db_bill
-- ----------------------------
DROP TABLE IF EXISTS `db_bill`;
CREATE TABLE `db_bill` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `uid` int(11) DEFAULT NULL,
  `blx` int(11) NOT NULL DEFAULT '0' COMMENT '操作类型',
  `bname` varchar(50) NOT NULL COMMENT '操作名称',
  `bdate` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP COMMENT '时间',
  `bz` text COMMENT '备注',
  `state` int(11) NOT NULL DEFAULT '0' COMMENT '状态,0待支付,1已支付,2退款',
  `isdel` int(11) NOT NULL DEFAULT '0' COMMENT '是否删除  0:否  1:是',
  PRIMARY KEY (`id`),
  KEY `uid` (`uid`),
  CONSTRAINT `db_bill_ibfk_1` FOREIGN KEY (`uid`) REFERENCES `db_users` (`id`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB AUTO_INCREMENT=95 DEFAULT CHARSET=utf8mb4;

-- ----------------------------
-- Records of db_bill
-- ----------------------------
INSERT INTO `db_bill` VALUES ('84', '1', '2', '提现', '2021-07-28 16:30:31', null, '1', '0');
INSERT INTO `db_bill` VALUES ('85', '1', '1', '充值', '2021-07-28 16:32:03', null, '1', '0');
INSERT INTO `db_bill` VALUES ('86', '11', '1', '充值', '2021-07-28 16:32:03', null, '1', '0');
INSERT INTO `db_bill` VALUES ('87', '11', '2', '提现', '2021-07-28 16:33:00', null, '1', '0');
INSERT INTO `db_bill` VALUES ('88', '11', '3', '转账', '2021-07-28 17:18:26', '转账', '1', '0');
INSERT INTO `db_bill` VALUES ('89', '1', '3', '转账', '2021-07-28 17:18:27', '转账', '1', '0');
INSERT INTO `db_bill` VALUES ('90', '1', '6', '消费', '2021-07-28 18:08:29', '消费', '1', '0');
INSERT INTO `db_bill` VALUES ('91', '1', '3', '转账', '2021-07-28 18:09:32', '转账', '1', '0');
INSERT INTO `db_bill` VALUES ('92', '11', '3', '转账', '2021-07-28 18:09:32', '转账', '1', '0');
INSERT INTO `db_bill` VALUES ('93', '1', '6', '消费', '2021-07-29 10:19:19', '消费', '1', '0');
INSERT INTO `db_bill` VALUES ('94', '1', '0', '奖金', '2021-07-29 16:34:05', null, '1', '0');

-- ----------------------------
-- Table structure for db_bill_amount
-- ----------------------------
DROP TABLE IF EXISTS `db_bill_amount`;
CREATE TABLE `db_bill_amount` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `bid` int(11) DEFAULT NULL COMMENT '账单id',
  `cid` int(11) DEFAULT NULL COMMENT '币种id',
  `amount` decimal(18,2) NOT NULL DEFAULT '0.00' COMMENT '金额',
  `state` int(11) NOT NULL DEFAULT '0' COMMENT '预留状态0无事发生',
  PRIMARY KEY (`id`),
  KEY `bid` (`bid`),
  KEY `cid` (`cid`),
  CONSTRAINT `db_bill_amount_ibfk_1` FOREIGN KEY (`bid`) REFERENCES `db_bill` (`id`) ON DELETE SET NULL ON UPDATE SET NULL,
  CONSTRAINT `db_bill_amount_ibfk_2` FOREIGN KEY (`cid`) REFERENCES `db_wallets_coin` (`id`) ON DELETE SET NULL ON UPDATE SET NULL
) ENGINE=InnoDB AUTO_INCREMENT=13 DEFAULT CHARSET=utf8mb4;

-- ----------------------------
-- Records of db_bill_amount
-- ----------------------------
INSERT INTO `db_bill_amount` VALUES ('1', '84', '3', '40.40', '0');
INSERT INTO `db_bill_amount` VALUES ('2', '85', '3', '1000.00', '0');
INSERT INTO `db_bill_amount` VALUES ('3', '86', '3', '10000.00', '0');
INSERT INTO `db_bill_amount` VALUES ('4', '87', '3', '1245.33', '0');
INSERT INTO `db_bill_amount` VALUES ('5', '88', '3', '100.00', '0');
INSERT INTO `db_bill_amount` VALUES ('6', '89', '3', '100.00', '0');
INSERT INTO `db_bill_amount` VALUES ('7', '90', '3', '320.00', '0');
INSERT INTO `db_bill_amount` VALUES ('8', '91', '3', '100.00', '0');
INSERT INTO `db_bill_amount` VALUES ('9', '92', '3', '100.00', '0');
INSERT INTO `db_bill_amount` VALUES ('10', '93', '3', '-20020.00', '0');
INSERT INTO `db_bill_amount` VALUES ('11', '94', '1', '100.00', '0');
INSERT INTO `db_bill_amount` VALUES ('12', '94', '1', '50.00', '0');

-- ----------------------------
-- Table structure for db_bonus
-- ----------------------------
DROP TABLE IF EXISTS `db_bonus`;
CREATE TABLE `db_bonus` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `btdate` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP,
  PRIMARY KEY (`id`),
  UNIQUE KEY `id` (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8mb4;

-- ----------------------------
-- Records of db_bonus
-- ----------------------------
INSERT INTO `db_bonus` VALUES ('3', '2021-07-23 00:00:00');

-- ----------------------------
-- Table structure for db_bonus_jiesuan
-- ----------------------------
DROP TABLE IF EXISTS `db_bonus_jiesuan`;
CREATE TABLE `db_bonus_jiesuan` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `userid` varchar(50) DEFAULT NULL,
  `lx` int(11) DEFAULT '0',
  `jdate` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `wdate` datetime DEFAULT NULL,
  `state` int(2) NOT NULL DEFAULT '0' COMMENT '0-正在结算,1-结算成功,2-结算失败',
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=10 DEFAULT CHARSET=utf8mb4;

-- ----------------------------
-- Records of db_bonus_jiesuan
-- ----------------------------
INSERT INTO `db_bonus_jiesuan` VALUES ('1', 'admin', '1', '2021-03-10 11:15:23', '2021-03-10 11:15:24', '1');
INSERT INTO `db_bonus_jiesuan` VALUES ('2', 'admin', '1', '2021-03-10 11:18:02', '2021-03-10 11:18:02', '1');
INSERT INTO `db_bonus_jiesuan` VALUES ('3', 'admin', '1', '2021-03-10 11:19:16', '2021-03-10 11:19:17', '1');
INSERT INTO `db_bonus_jiesuan` VALUES ('4', 'admin', '1', '2021-03-10 11:19:31', '2021-03-10 11:19:33', '1');
INSERT INTO `db_bonus_jiesuan` VALUES ('5', '1', '1', '2021-03-11 10:44:47', '2021-03-11 10:44:48', '1');
INSERT INTO `db_bonus_jiesuan` VALUES ('6', 'admin', '1', '2021-06-24 11:38:52', '2021-06-24 11:38:52', '0');
INSERT INTO `db_bonus_jiesuan` VALUES ('7', 'admin', '1', '2021-06-24 11:39:53', '2021-06-24 11:39:53', '0');
INSERT INTO `db_bonus_jiesuan` VALUES ('8', 'admin', '1', '2021-06-24 11:40:22', '2021-06-24 11:40:22', '0');
INSERT INTO `db_bonus_jiesuan` VALUES ('9', 'admin', '1', '2021-06-25 10:29:54', '2021-06-25 10:29:54', '0');

-- ----------------------------
-- Table structure for db_bonus_source
-- ----------------------------
DROP TABLE IF EXISTS `db_bonus_source`;
CREATE TABLE `db_bonus_source` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `btid` int(11) DEFAULT NULL,
  `yid` int(11) NOT NULL DEFAULT '0' COMMENT '来源uid',
  `yuserid` varchar(50) NOT NULL COMMENT '来源userid',
  `yusername` varchar(50) NOT NULL COMMENT '来源username',
  `uid` int(11) NOT NULL DEFAULT '0' COMMENT '获奖uid',
  `userid` varchar(50) NOT NULL COMMENT '获奖userid',
  `username` varchar(50) NOT NULL,
  `lx` int(11) NOT NULL DEFAULT '0' COMMENT '获奖类型',
  `jine` decimal(18,2) NOT NULL DEFAULT '0.00' COMMENT '金额',
  `cid` int(11) NOT NULL DEFAULT '1' COMMENT '币种id,通常为1=奖金',
  `bz` varchar(100) NOT NULL,
  `state` int(11) NOT NULL DEFAULT '0' COMMENT '类型0-未结算,1已结算',
  `sdate` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP COMMENT '写入日期',
  `edate` datetime DEFAULT NULL COMMENT '结算日期',
  PRIMARY KEY (`id`),
  KEY `yid` (`yid`),
  KEY `uid` (`uid`),
  KEY `btid` (`btid`),
  KEY `state` (`state`) USING BTREE,
  CONSTRAINT `db_bonus_source_ibfk_1` FOREIGN KEY (`btid`) REFERENCES `db_bonus` (`id`) ON DELETE SET NULL ON UPDATE SET NULL
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8mb4;

-- ----------------------------
-- Records of db_bonus_source
-- ----------------------------
INSERT INTO `db_bonus_source` VALUES ('1', '3', '11', 'aaa', '1', '1', 'admin', '管理员', '1', '500.00', '1', '-', '0', '2021-07-23 16:20:21', '2021-07-26 22:39:55');
INSERT INTO `db_bonus_source` VALUES ('2', '3', '12', 'bbb', '123456', '11', 'aaa', '1', '1', '500.00', '1', '-', '0', '2021-07-23 16:21:23', '2021-07-26 22:39:59');
INSERT INTO `db_bonus_source` VALUES ('3', '3', '13', 'ccc', '123456', '1', 'admin', '管理员', '1', '500.00', '1', '-', '0', '2021-07-23 16:21:27', '2021-07-26 22:40:02');

-- ----------------------------
-- Table structure for db_checkcode
-- ----------------------------
DROP TABLE IF EXISTS `db_checkcode`;
CREATE TABLE `db_checkcode` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `code` varchar(50) DEFAULT NULL,
  `usertel` varchar(50) DEFAULT NULL,
  `cdate` datetime DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- ----------------------------
-- Records of db_checkcode
-- ----------------------------

-- ----------------------------
-- Table structure for db_help
-- ----------------------------
DROP TABLE IF EXISTS `db_help`;
CREATE TABLE `db_help` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `zan` int(11) DEFAULT '0',
  `cai` int(11) DEFAULT '0',
  `question` varchar(100) DEFAULT NULL,
  `answer` longtext,
  `show` int(11) DEFAULT '0',
  `hpath` longtext COMMENT '点击时出现的问题',
  `hlevel` int(11) DEFAULT '0',
  `gpath` longtext COMMENT '记录别的问题里hpath有此问题的id',
  `rank` int(11) NOT NULL DEFAULT '0' COMMENT '排序',
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=52 DEFAULT CHARSET=utf8mb4;

-- ----------------------------
-- Records of db_help
-- ----------------------------
INSERT INTO `db_help` VALUES ('48', '0', '0', '问题1', '答案1', '1', ',49,', '1', ',', '1');
INSERT INTO `db_help` VALUES ('49', '0', '0', '问题2', '答案2', '1', ',', '0', ',48,', '1');
INSERT INTO `db_help` VALUES ('50', '0', '0', '问题3', '答案3', '1', ',', '1', ',', '1');
INSERT INTO `db_help` VALUES ('51', '0', '0', '问题4', '答案4', '1', ',', '0', ',', '2');

-- ----------------------------
-- Table structure for db_msg
-- ----------------------------
DROP TABLE IF EXISTS `db_msg`;
CREATE TABLE `db_msg` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `fid` int(11) DEFAULT '0',
  `fuserid` varchar(50) DEFAULT NULL,
  `lx` int(11) DEFAULT '0',
  `title` varchar(100) DEFAULT NULL,
  `msgcontent` longtext,
  `sid` int(11) DEFAULT '0',
  `suserid` varchar(50) DEFAULT NULL,
  `mdate` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `isread` int(11) DEFAULT '0',
  `sisdelete` int(11) DEFAULT '0',
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=75 DEFAULT CHARSET=utf8mb4;

-- ----------------------------
-- Records of db_msg
-- ----------------------------
INSERT INTO `db_msg` VALUES ('42', '0', '系统消息', '0', '账户激活', '您的账户已成功激活', '11', 'aaa', '2021-07-23 16:20:22', '0', '0');
INSERT INTO `db_msg` VALUES ('43', '0', '系统消息', '0', '账户激活', '您的账户已成功激活', '12', 'bbb', '2021-07-23 16:21:24', '0', '0');
INSERT INTO `db_msg` VALUES ('44', '0', '系统消息', '0', '账户激活', '您的账户已成功激活', '13', 'ccc', '2021-07-23 16:21:28', '0', '0');
INSERT INTO `db_msg` VALUES ('45', '0', '系统消息', '0', '提现', '您的提现申请:1233.00已通过审核', '11', 'aaa', '2021-08-09 11:34:07', '0', '0');
INSERT INTO `db_msg` VALUES ('46', '0', '系统消息', '0', '提现', '您的提现申请:40.00已通过审核', '1', 'admin', '2021-08-09 11:34:13', '1', '0');
INSERT INTO `db_msg` VALUES ('47', '0', '系统消息', '0', '提现', '您的提现申请:1233.00已通过审核', '11', 'aaa', '2021-08-09 11:35:09', '0', '0');
INSERT INTO `db_msg` VALUES ('48', '0', '系统消息', '0', '提现', '您的提现申请:1233.00已通过审核', '11', 'aaa', '2021-08-09 11:35:25', '0', '0');
INSERT INTO `db_msg` VALUES ('49', '0', '系统消息', '0', '提现', '您的提现申请:1233.00已通过审核', '11', 'aaa', '2021-08-09 11:44:26', '0', '0');
INSERT INTO `db_msg` VALUES ('50', '0', '系统消息', '0', '提现', '您的提现申请:1233.00已通过审核', '11', 'aaa', '2021-08-26 09:32:55', '0', '0');
INSERT INTO `db_msg` VALUES ('51', '0', '系统消息', '0', '提现', '您的提现申请:1233.00已通过审核', '11', 'aaa', '2021-08-26 09:53:01', '0', '0');
INSERT INTO `db_msg` VALUES ('52', '0', '系统消息', '0', '提现', '您的提现申请:1233.00已通过审核', '11', 'aaa', '2021-08-26 09:58:04', '0', '0');
INSERT INTO `db_msg` VALUES ('53', '0', '系统消息', '0', '提现', '您的提现申请:1233.00已通过审核', '11', 'aaa', '2021-08-26 10:00:32', '0', '0');
INSERT INTO `db_msg` VALUES ('54', '0', '系统消息', '0', '提现', '您的提现申请:1233.00已通过审核', '11', 'aaa', '2021-08-26 10:05:35', '0', '0');
INSERT INTO `db_msg` VALUES ('55', '0', '系统消息', '0', '提现', '您的提现申请:1233.00已通过审核', '11', 'aaa', '2021-08-26 10:09:49', '0', '0');
INSERT INTO `db_msg` VALUES ('56', '0', '系统消息', '0', '提现', '您的提现申请:1233.00已通过审核', '11', 'aaa', '2021-08-26 10:14:09', '0', '0');
INSERT INTO `db_msg` VALUES ('57', '0', '系统消息', '0', '提现', '您的提现申请:1233.00已通过审核', '11', 'aaa', '2021-08-26 10:17:20', '0', '0');
INSERT INTO `db_msg` VALUES ('58', '0', '系统消息', '0', '提现', '您的提现申请:1233.00已通过审核', '11', 'aaa', '2021-08-26 10:21:05', '0', '0');
INSERT INTO `db_msg` VALUES ('59', '0', '系统消息', '0', '提现', '您的提现申请:1233.00已通过审核', '11', 'aaa', '2021-08-26 10:26:28', '0', '0');
INSERT INTO `db_msg` VALUES ('60', '0', '系统消息', '0', '订单', '您的订单:20210729101918191758616949已发货', '1', 'admin', '2021-08-26 10:29:41', '1', '0');
INSERT INTO `db_msg` VALUES ('61', '0', '系统消息', '0', '提现', '您的提现申请:1233.00已通过审核', '11', 'aaa', '2021-08-26 10:30:52', '0', '0');
INSERT INTO `db_msg` VALUES ('62', '0', '系统消息', '0', '提现', '您的提现申请:1233.00已通过审核', '11', 'aaa', '2021-08-26 10:32:27', '0', '0');
INSERT INTO `db_msg` VALUES ('63', '0', '系统消息', '0', '提现', '您的提现申请:1233.00已通过审核', '11', 'aaa', '2021-08-26 10:33:40', '0', '0');
INSERT INTO `db_msg` VALUES ('64', '0', '系统消息', '0', '提现', '您的提现申请:1233.00已通过审核', '11', 'aaa', '2021-08-26 10:37:21', '0', '0');
INSERT INTO `db_msg` VALUES ('65', '0', '系统消息', '0', '提现', '您的提现申请:40.00已通过审核', '1', 'admin', '2021-08-26 10:37:21', '1', '0');
INSERT INTO `db_msg` VALUES ('66', '0', '系统消息', '0', '提现', '您的提现申请:1233.00已通过审核', '11', 'aaa', '2021-08-26 10:38:08', '0', '0');
INSERT INTO `db_msg` VALUES ('67', '0', '系统消息', '0', '提现', '您的提现申请:40.00已通过审核', '1', 'admin', '2021-08-26 10:38:21', '1', '0');
INSERT INTO `db_msg` VALUES ('68', '0', '系统消息', '0', '提现', '您的提现申请:1233.00已通过审核', '11', 'aaa', '2021-08-26 10:43:12', '0', '0');
INSERT INTO `db_msg` VALUES ('69', '0', '系统消息', '0', '提现', '您的提现申请:40.00已通过审核', '1', 'admin', '2021-08-26 10:43:13', '1', '0');
INSERT INTO `db_msg` VALUES ('70', '0', '系统消息', '0', '订单', '您的订单:20210728180828287820933323已发货', '1', 'admin', '2021-08-26 10:44:20', '1', '0');
INSERT INTO `db_msg` VALUES ('71', '0', '系统消息', '0', '提现', '您的提现申请:40.00已通过审核', '1', 'admin', '2021-08-26 10:47:22', '1', '0');

-- ----------------------------
-- Table structure for db_news
-- ----------------------------
DROP TABLE IF EXISTS `db_news`;
CREATE TABLE `db_news` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `newscover` varchar(255) NOT NULL COMMENT '封面图',
  `newstitle` varchar(255) DEFAULT NULL,
  `newsoperator` varchar(50) DEFAULT NULL,
  `newscontent` longtext,
  `newstime` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `display` int(11) DEFAULT '0',
  `clicks` int(11) DEFAULT '0',
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=26 DEFAULT CHARSET=utf8mb4;

-- ----------------------------
-- Records of db_news
-- ----------------------------
INSERT INTO `db_news` VALUES ('22', 'admin60d40ec7-b97f-49ff-b1f8-c08ba962d648.jpg', '黄鳝不吃头，田螺不吃尾”，在民间的这些饮食禁忌，你听过吗？', 'admin', '<p><img src=\"admin0bf01ec5-a555-4004-be08-c763239676c1.jpg\" style=\"max-width:100%;\"><br></p><p>“黄鳝不吃头，田螺不吃尾”，在民间的这些饮食禁忌，你听过吗？！<br><br>生活中这些科学事件和这些奇怪的现象，在我们。身边也是非常的多啦，大多数的人会选择一个敬而远之，这样的一个观点，有这样一个事情出现呢，我是半信半疑的，然后呢，对他保持一个敬畏的态度。大到什么新闻，玄学，小到生活中的方方面面，酒要怎么喝，茶要怎么泡，过什么节日，应该做什么事，这不仅有讲究，有说道，而且都是要遵守的。<br><br><br>拿我自己来举例子吧，小的时候我妈妈就经常告诉过在我们家里面的盖帘，用的时候，只能先放水在锅里，不能穿过盖帘把水漏下去，具体什么原因也没告诉我，还有就是等等类似的一些小事。比如说，过年的时候，要高高兴兴地，不能哭，想一点好事，会给一年带来好运气。本命年穿红色，其实这都能算得上是生活中的大讲究。再有就是结婚，我们都知道结婚的时候，有很多说法，新娘要怎么样，新郎来的时候，这些个环节都是讲究，新婚铺被子也有说法，其实这些我们也都是知道的，并且也接受了我国这种传统。<br><br><br>70<br><br>%<br><br>那么在吃东西方面，也有一些说法。吃东西怎么吃，这算不上。假如说，吃土豆不吃皮。这根本不算，毕竟有些土豆可以吃皮。还有鱼不吃腮，这些都是吃法，而不是说法。吃法是一个过程，是什么东西可以吃，什么东西不可以吃的简单程序。而像吃黄鳝不吃头这种，就不是程序或者规则了。而是带着故事的一种说法。<br><br><br>在很久以前了，吃黄鳝不吃头是因为在黄鳝里面有一种呢叫做望月鳝，也就是说这种望月鳝会经常从水面上浮出来，然后抬着自己的头看向月亮。不得不说呀，以前的黄鳝真的是也是一种闲情逸致呢，但是后来也有人说这些黄鳝呢，他们会在一些被水没过的坟墓旁边游走，那么他们也非常喜欢吃这种腐烂的肉，而且长期下来，以至于他们在深深的地底。<br><img src=\"admin738da27f-62b5-4126-8aec-82878bc5215c.jpg\" style=\"max-width:100%;\"><br><br>头部就是含有剧毒的，所以以前人也是不吃这种黄鳝头的。都是说吃了就会中毒。不过，这也是很久之前的民间传说了，作为传说的话，它的科学依据，我们也是有待参考的不一定。但也不一定就是假的，现在我们对待传说，大多数都是这个看法，不过我们现在的人越是不吃黄鳝头的，然而却是有实际上的科学依据的。<br><br><br>为什么不吃黄鳝头呢？其实主要就是因为这个黄鳝吧，她的头非常的脏的。像黄鳝，他经常在离泥里行走，而他的头了，肯定会吃一些脏的东西，那么尤其是向头部这种位置，它本身就没有多少肉，再加上里面还有可能含有重金属，我们吃了的话会对身体产生一些反应，或者是一些坏处，所以大多数的人也不去吃，它的头一般都是只吃身体，不吃头。<br><br><br>还有一个另外的说法就是我们吃田螺的时候不能吃尾巴，田螺的这个尾巴，有一些人那是直接就吃掉了的，但是大部分人还是不吃尾巴的，因为田螺他也是生活在海里的。尾部的话，不仅会有一些脏东西，还有可能会有一些残留物，尤其是沙子呀，还有这种田螺卵啊，都会在它的尾部里面出现。尾部的话，很有可能会对人体造成一些不必要的伤害。<br><br><img src=\"admin93442f8e-ebbf-4a0c-99fe-94c1be3d1f49.jpg\" style=\"max-width:100%;\"><br>拉肚子，那就是最简单了的了，也有很多朋友说呀，吃到这个田螺的卵，或者是沙子的时候口味特别的怪异，而且非常的难受，所以说尾部一般都是不吃的，而且像田螺这种海里面的生物，如果它的尾部里面有一些细菌的话，即使是高温炒制也不一定会起到一个杀菌的作用。不过大多数的朋友还是不知道这个事情的，也不知道下面是什么东西，所以说以为可以吃，直接就吃了，但是最好还是不要吃田螺的尾部。</p><p></p>', '2020-10-28 14:40:03', '1', '9');
INSERT INTO `db_news` VALUES ('23', 'admind8237122-4a2b-42c5-ae3a-261378817490.png', '如果你准备无限期地采用生酮饮食法或尝试其他健康食谱……', 'admin', '<p><img src=\"admin1fa42319-1440-42cc-8ab0-eb86d594a39b.jpg\" style=\"max-width:100%;\"><br></p><p>如果你准备无限期地采用生酮饮食法或尝试其他健康食谱，那就去做吧。但是说实话，渐进式的饮食改变才是可持续的。首先你要抛弃碳酸饮料，转而选择多喝白开水。</p>', '2020-10-28 14:41:31', '1', '8');
INSERT INTO `db_news` VALUES ('24', 'adminafb05007-7b4d-4090-b8ad-f10f324baa72.jpg', '从“人带货”到“货带人”，直播电商的逻辑变了', 'admin', '<img src=\"admin0e4829f6-d4bc-4824-85ee-702c744ed5d4.jpg\" style=\"max-width:100%;\"><br>1、直播电商发展远未达到拐点，将继续保持高速增长态势;<br><br>2、供应链能力是决定主播生死的关键性因素，不能拉到品牌、压低价格的主播将被很快淘汰;<br><br>3、直播电商的成长速度太快，数据造假、虚假宣传、质量问题等还需尽快规范。<br><br>“起来!醒!今晚这觉睡不得，一睡几百块就没了!”<br><br>“人间闹钟”李佳琦的催促下，上亿网友守在直播前下单抢货。谁都没有想到，今年双十一的开场如此热闹。<br><br>淘宝直播榜单显示，在10月20晚至10月21日凌晨的直播中，李佳琦和薇娅分别获得了1.62亿和1.48亿人次的累计观看量。销售额总和近70亿元，相当于国庆档全国电影票房(39.52亿元)的近2倍。<br><br>这也是数月以来，李佳琦在直播数据上***超越薇娅，成为“双十一”预售首日的“最强打工人”。<br><br>经历了因病停播、助理单飞、过气质疑之后，李佳琦再次证明了自己直播带货“一哥”的实力。也让人们看到，直播电商的发展还远未到顶。<br><br><br>万亿市场拐点未至<br><br>2016年被称为直播元年，李佳琦在这一年签约美ONE成为一名美妆主播。<br><br>淘宝在直播带货上的探索也是从这一年开始。“2016-2018年淘宝直播带动的成交额增速一直在150%以上，只不过之前绝对的交易规模相对小一些，没有受到资本市场与消费大众的广泛关注。”淘宝直播MCN运营负责人李明介绍。<br><br>2019年，直播带货在各大平台全面爆发。“两超多强”的主播生态圈逐步建立，薇娅成为淘宝平台内***的主播，李佳琦则借助抖音短视频火速出圈。<br><br>2020年的疫情，更是把直播电商推上风口浪尖。艾瑞咨询调查报告显示，疫情期间网络直播用户规模激增，有近3成受访者几乎每天都看带货直播。<br><br>阿里研究院产业研究中心主任郝建彬认为，直播电商的价值源泉有三个。<br><br>首先，在于规模效益。以往电商的销售是碎片化的，现在直播间里几分钟就会发生上百万单的销量，在这种规模效益下，成本一定会降低。<br><br>其次，在于去中介化。原来的产业连锁在线下，经过多级加价分销;现在通过直播间把商品直接转到消费者手中，压缩了中间渠道，用户得以享受低价福利。<br><br>最后，在于服务溢价。在线下，李佳琦一次只能服务一个客户，但在线上直播中可以触达上百万甚至上千万人。在这个过程中，大家实际上都是在以零成本享受高端服务。<br><br>从领取优惠券、全场五折、满减活动，到口令红包、组队PK、养猫升级，这些年来电商平台的“套路”越来越多。而李佳琦们扮演的角色更像是消费者的朋友，他们帮你试色、替你讲价、提醒你抢购，让人们省去不少“算账”的时间。<br><br>“李佳琦会告诉我们怎么买是最划算的。”这是众多网友打着瞌睡、熬着夜还要蹲守在李佳琦直播间前的重要原因。<br><br>就在不久前，李佳琦还因为种种原因深陷质疑。<br><br>一方面是流量被瓜分。今年以来，一向敬业的他在直播场次、观看人数等数据上都不如薇娅，更是因为身体屡出状况而多次停播。<br><br>另一方面是负面消息缠身，叫错嘉宾名字、不粘锅直播“翻车”、助理付鹏单飞，唱衰李佳琦的声音不绝于耳。<br><br>与此同时，疫情后人们生活逐渐回到正轨，对直播的关注度开始下降。再加上频上热搜的明星直播翻车事件，人们开始怀疑，难道直播电商的火爆只是“昙花一现”?<br><br>如今，李佳琦凭借“双十一”预售首日的战绩证明了自己“一哥”的实力，而这还仅仅是活动的开始。<br><br>“现在电商直播行业的发展还远远没有达到行业拐点的阶段。”李明说，“2019年天猫双十一的成交额是2635亿元，其中淘宝直播引导的成交额不到200亿元，比例仅为7%，所以直播电商还有很大的发展空间。”<br><br>毕马威和阿里研究院联合发布的报告显示，2020年直播电商整体规模将达到1.05万亿元，且继续保持高速增长态势，到2021年将扩大至近2万亿元。<br><br>从“人带货”到“货带人”<br><br>随着各家平台对流量的争夺进入白热化，直播电商的带货逻辑也开始发生微妙的变化。<br><br>直播电商相比传统电商的一大优势在于链路更短、转化率更高，但归根结底主播也是商家和消费者之间的“中间商”，这个链路仍有能被缩短的空间。<br><br>一个明显的趋势是，淘宝正在从扶持主播带货转向引导商家自播。<br><br>商家自播是目前淘宝直播的中流砥柱，也是淘宝独有的直播生态。越来越多的商家们意识到直播可以带动销量增长，便开始尝试自己做直播。<br><br>“疫情其实把这个过程加速了，我们曾经认为可能不太适合电商直播的行业，尤其是3C数码这样偏标品的行业，在今年电商直播的成长其实非常快。”李明说，“商家会成为我们非常重要的爆发点。”<br><br>在这个赛道里，目前淘宝的对手不是拼多多，而是抖音和快手。<br><br>快手从2018年开始试水电商业务，2019年交易额超过350亿元，2020年的目标是2500亿元。“快手一哥”辛巴坐拥超过6500万粉丝，2019年直播带货总交易额达133亿元。<br><br>定制化的供应链体系是其高销量的重要支撑。辛巴经常强调，只有拥有优秀的供应链才能撑起优质的直播电商内容，“没有好的货带不来人，货比什么都重要”。<br><br>抖音直播电商起步较晚但成长迅速。尤其是今年疫情之后，接连签约罗永浩、王祖蓝、陈赫等明星大咖进行直播带货，还发布了一系列政策扶持新人入驻。<br><br>抖音的推荐机制更加突出“货带人”的特点。当用户搜索某一商品，抖音会推送一批销售同样商品的直播间，让用户“货比三家”。质量不够好、价格不够便宜，就会很容易被用户Pass。<br><br>可见，直播带货最终还是要在“货”上下功夫。供应链能力成为决定主播生死的关键性因素，拿不到足够多的品牌和足够低的价格，就势必会在激烈的竞争中落败。<br><br>这也是为什么李佳琦和薇娅在选品方面极其严格，在价格方面更是锱铢必较。李佳琦团队工作人员曾透露，“有一个百人规模的吹毛求疵团队”，“每100件产品中，大约只有5%会留下”。李佳琦本人还曾在直播中向品牌喊话：“要做就做***，不做就不要参加双十一。”<br><br>图源：新浪微博@李佳琦Austin<br><br>但价格过低挤占了商家的利润空间，品牌和主播的矛盾不断爆发。有商家曾吐槽李佳琦，“双十一”当天的链接费为15万，分成比例为20%，他们和李佳琦合作了5次亏了3次，“双十一”当天更是亏了50万。<br><br>水分该挤一挤了<br><br>众多第三方机构对李佳琦和薇娅直播数据的统计口径和结果不尽相同，但基本都算到了30亿元以上，观看人次加总在3亿左右。不过，有不少人对这些数据提出了质疑。<br><br>比如这并非意味着有共3亿人观看了二人的直播。因为数据并未去重，且统计的是累计观看次数，而有不少消费者是在两个直播间来回切换，每进一次直播间就被计算为一次观看，实际观看人数难以估算。<br><br>在电商行业分析师李成东看来，主播们的销售额也普遍虚高。因为大多数主播发官方战报时给出的都是下单金额，实际支付金额和最终完成订单并没有那么多。在各个平台披露的GMV数值中，约有30%的未支付订单、10%的退货订单和10%的刷单。<br><br>他认为，即使是李佳琦和薇娅，真实数据可能也只有披露金额的六七成。这可能还算是高的，因为还有不少主播为了提高坑位费找人刷单，则是完全的数据造假。<br><br>除此之外，质量问题、虚假宣传、货不对板、赠品不兑现等，也是直播电商被诟病最多的问题。<br><br>去年9月，李佳琦就因为将“阳澄大闸蟹”宣传成为“阳澄湖大闸蟹”，被消费者质疑虚假宣传。尽管工作室很快发表声明解释并致歉，但还是造成了不小的负面影响。<br><br>而今年“双十一”预售活动中，李佳琦再次被指所售某大牌化妆品供货渠道不正。同样的产品，在李佳琦直播间售价789元，和薇娅直播间的1080元相差291元。大多数平台和主播售卖的同款商品，价格都比这要高出不少。<br><br>有网友咨询该品牌官方旗舰店客服，得到的答复却是“并没有在李佳琦的直播间做活动”。为李佳琦直播间供货的实为某海淘网站，并非品牌官方。<br><br>直播电商的成长速度太快，在管理规范方面亟需不断完善。<br><br>10月20日，国家市场监管总局公布《网络交易监督管理办法(征求意见稿)》。其中明确“网络社交、网络直播等其他网络服务提供者在满足一定条件时应当依法履行网络交易平台经营者的责任”，并规定“网络直播服务提供者应当为利用网络直播开展的网络交易活动提供回看功能”。<br><br>“随着直播电商的持续火爆，纳入更严格的监管是可以预见的。”对此，网经社电子商务研究中心特约研究员、北京盈科(杭州)律师事务所股权高级合伙人黄伟律师表示。<br><br>监管的严要求会一定程度上加剧整个行业的优胜劣汰。“头部主播因为更为专业团队的辅助，能够更好地适应整个监管环境，而一些实力不强、产品销售质量参差不齐的小主播可能会渐渐面临市场淘汰。”<br><br>结语<br><br>目前直播领域马太效应严重，前2%的头部主播占据了近80%的市场份额。<br><br>排在“双十一”预售首日带货榜单第三名的淘宝主播雪梨，已经在销售额、观看人次、粉丝量等各方面数据指标上，被薇娅、李佳琦拉开差距，更不用说再往后的主播。<br><br>头部主播早已超越带货，他们本身也已经成为一种品牌。<br><br>李佳琦已经和完美日记、花西子等国货彩妆品牌深度绑定，开发出多款联名产品。薇娅忙着上综艺、为公益助农站台，扩大自身影响力。辛巴则把更多精力放在供应链上，先后上线“辛有志严选”和“辛选供应链平台”。<br><br>万亿规模的直播电商产业链里，他们都不再甘于做一个“打工人”。<p></p>', '2020-10-28 14:43:05', '1', '6');
INSERT INTO `db_news` VALUES ('25', 'admin89fd3d93-a60e-4728-ac5f-1b95a55b5b1a.jpg', '头顶80亿库存，周杰伦能否拯救海澜之家？', 'admin', '<img src=\"http://appserver.gxyhttest.com/Upload/adminc7e76ca8-2dd7-4439-947f-13996379aef5.jpg\" style=\"max-width:100%;\"><br>周杰伦能否拯救海澜之家<br><br>回到文章开头的那个问题，周杰伦可以挽救海澜之家吗？<br><br>答案也许是否定的。<br><br>高流量的代言人并不是拯救一个逐渐老化的服装品牌的“万能药”。<br><br>高喊着“不走寻常路”的美邦也曾与周杰伦深度绑定，但面临国内外品牌和互联网的双重冲击，依旧免不了衰败的命运：仅2020年上半年，美邦净亏损4亿元，关闭店铺504家，公司负债达40亿元。<br><br>归根结底，营销仅仅只是一个品牌成功的一个要素，产品、供应链、战略等等其他层面同样发挥着重要的作用。<br><br>周杰伦的代言，无疑会给此刻内外交困的海澜之家带来许多关注和流量，但这个国民男装品牌，是否能重现往日的辉煌，仍是一个未知数。<p></p>', '2020-10-28 14:44:05', '1', '70');

-- ----------------------------
-- Table structure for db_shop_collection
-- ----------------------------
DROP TABLE IF EXISTS `db_shop_collection`;
CREATE TABLE `db_shop_collection` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `uid` int(11) NOT NULL DEFAULT '0',
  `spath` longtext NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- ----------------------------
-- Records of db_shop_collection
-- ----------------------------

-- ----------------------------
-- Table structure for db_shop_goods
-- ----------------------------
DROP TABLE IF EXISTS `db_shop_goods`;
CREATE TABLE `db_shop_goods` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `goodsNo` varchar(50) DEFAULT NULL,
  `goodsname` varchar(255) DEFAULT NULL,
  `goodstype` int(11) NOT NULL DEFAULT '0',
  `goodsprice` decimal(18,2) NOT NULL DEFAULT '0.00',
  `goodspv` decimal(18,2) NOT NULL DEFAULT '0.00',
  `stock` int(11) NOT NULL DEFAULT '0' COMMENT '库存',
  `sales` int(11) NOT NULL DEFAULT '0' COMMENT '销量',
  `goodscontent` longtext,
  `goodsimg` longtext,
  `goodsdate` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `ispay` int(11) NOT NULL DEFAULT '0',
  `dlid` int(11) NOT NULL DEFAULT '0',
  `dlname` varchar(100) DEFAULT NULL,
  `xlid` int(11) NOT NULL DEFAULT '0',
  `xlname` varchar(100) DEFAULT NULL,
  `goodslabel` longtext,
  `cost` decimal(18,2) NOT NULL DEFAULT '0.00' COMMENT '运费',
  `prompt` varchar(100) DEFAULT NULL,
  `sort` int(11) NOT NULL DEFAULT '0' COMMENT '排序',
  `tj1` decimal(10,0) NOT NULL DEFAULT '0',
  `tj2` decimal(10,0) NOT NULL DEFAULT '0',
  `tj3` decimal(10,0) NOT NULL DEFAULT '0',
  `tj4` decimal(10,0) NOT NULL DEFAULT '0',
  `tj5` decimal(10,0) NOT NULL DEFAULT '0',
  `ishome` int(10) NOT NULL DEFAULT '0' COMMENT '是否首页展示',
  PRIMARY KEY (`id`),
  KEY `xlid` (`xlid`)
) ENGINE=InnoDB AUTO_INCREMENT=69 DEFAULT CHARSET=utf8mb4;

-- ----------------------------
-- Records of db_shop_goods
-- ----------------------------
INSERT INTO `db_shop_goods` VALUES ('66', '003', '寡肽修护冻干粉（3ml×10组）', '1', '320.00', '115.00', '977', '23', '<p><br></p><p></p>', 'admin663fae26-bf76-41d3-8278-008ed300db78.JPG', '2020-10-31 12:43:55', '1', '21', '大类2', '43', '食品', '', '0.00', '补水', '534', '0', '0', '0', '0', '0', '1');
INSERT INTO `db_shop_goods` VALUES ('67', '004', '寡肽修护冻干粉（3ml×30组）', '0', '958.00', '336.00', '967', '33', '<p><ppingfang sc\';=\"\" #000000\"=\"\">寡肽修护冻干粉成分：甘露糖醇、寡肽-1、寡肽-2、寡肽-3。</ppingfang></p><p></p><p><ppingfang sc\';=\"\" #000000\"=\"\"><ppingfang sc\';=\"\" #000000\"=\"\">溶媒液成分：水、透明质酸钠、1,2-己二醇。&nbsp;</ppingfang></ppingfang></p><p></p><p><ppingfang sc\';=\"\" #000000\"=\"\"><ppingfang sc\';=\"\" #000000\"=\"\"><ppingfang sc\';=\"\" #000000\"=\"\">功效：蕴含寡肽成分，能有效进行深度的修复干燥、粗糙等肌肤问题，丰富的活性成分使肌肤水嫩清透，长期使用使肌肤表层锁水、持续滋养、肌底贮水。</ppingfang></ppingfang></ppingfang></p><p><ppingfang sc\';=\"\" #000000\"=\"\"><ppingfang sc\';=\"\" #000000\"=\"\"><ppingfang sc\';=\"\" #000000\"=\"\"><ppingfang sc\';=\"\" #000000\"=\"\">寡肽1的功效：可用于各种美容修饰后的肌肤修复，以及皮肤粗糙、肤质不平滑的修复及日常保养，改善薄弱敏感肌肤。</ppingfang></ppingfang></ppingfang></ppingfang></p><p></p><p><ppingfang sc\';=\"\" #000000\"=\"\"><ppingfang sc\';=\"\" #000000\"=\"\"><ppingfang sc\';=\"\" #000000\"=\"\"><ppingfang sc\';=\"\" #000000\"=\"\"><ppingfang sc\';=\"\" #000000\"=\"\">寡肽2的功效：有效舒缓修复，改善薄弱敏感皮肤、提升皮肤自我修护能力，淡化已有痘印，拦截未形成的黑色素。</ppingfang></ppingfang></ppingfang></ppingfang></ppingfang></p><p></p><p><ppingfang sc\';=\"\" #000000\"=\"\"><ppingfang sc\';=\"\" #000000\"=\"\"><ppingfang sc\';=\"\" #000000\"=\"\"><ppingfang sc\';=\"\" #000000\"=\"\"><ppingfang sc\';=\"\" #000000\"=\"\"><ppingfang sc\';=\"\" #000000\"=\"\">寡肽3的功效：渗透肌底，营造肌肤防护层，为肌肤注入强韧活力，调理皮肤，令肌肤恢复健康弹性。</ppingfang></ppingfang></ppingfang></ppingfang></ppingfang></ppingfang></p><p><ppingfang sc\';=\"\" #000000\"=\"\"><ppingfang sc\';=\"\" #000000\"=\"\"><ppingfang sc\';=\"\" #000000\"=\"\"><ppingfang sc\';=\"\" #000000\"=\"\"><ppingfang sc\';=\"\" #000000\"=\"\"><ppingfang sc\';=\"\" #000000\"=\"\"><ppingfang sc\';=\"\" #000000\"=\"\">注意事项：</ppingfang></ppingfang></ppingfang></ppingfang></ppingfang></ppingfang></ppingfang></p><p></p><p><ppingfang sc\';=\"\" #000000\"=\"\"><ppingfang sc\';=\"\" #000000\"=\"\"><ppingfang sc\';=\"\" #000000\"=\"\"><ppingfang sc\';=\"\" #000000\"=\"\"><ppingfang sc\';=\"\" #000000\"=\"\"><ppingfang sc\';=\"\" #000000\"=\"\"><ppingfang sc\';=\"\" #000000\"=\"\"><ppingfang sc\';=\"\" #000000\"=\"\">1.未开启的冻干粉请放置于阴凉干燥处；</ppingfang></ppingfang></ppingfang></ppingfang></ppingfang></ppingfang></ppingfang></ppingfang></p><p></p><p><ppingfang sc\';=\"\" #000000\"=\"\"><ppingfang sc\';=\"\" #000000\"=\"\"><ppingfang sc\';=\"\" #000000\"=\"\"><ppingfang sc\';=\"\" #000000\"=\"\"><ppingfang sc\';=\"\" #000000\"=\"\"><ppingfang sc\';=\"\" #000000\"=\"\"><ppingfang sc\';=\"\" #000000\"=\"\"><ppingfang sc\';=\"\" #000000\"=\"\"><ppingfang sc\';=\"\" #000000\"=\"\">2.调配后的冻干粉，请放于冰箱冷藏室（2-8<spanlucida grande\'\"=\"\">℃）保存，并请于24小时内用完；</spanlucida></ppingfang></ppingfang></ppingfang></ppingfang></ppingfang></ppingfang></ppingfang></ppingfang></ppingfang></p><p></p><p><ppingfang sc\';=\"\" #000000\"=\"\"><ppingfang sc\';=\"\" #000000\"=\"\"><ppingfang sc\';=\"\" #000000\"=\"\"><ppingfang sc\';=\"\" #000000\"=\"\"><ppingfang sc\';=\"\" #000000\"=\"\"><ppingfang sc\';=\"\" #000000\"=\"\"><ppingfang sc\';=\"\" #000000\"=\"\"><ppingfang sc\';=\"\" #000000\"=\"\"><ppingfang sc\';=\"\" #000000\"=\"\"><spanlucida grande\'\"=\"\"><ppingfang sc\';=\"\" #000000\"=\"\">3.请勿与含有重金属的膏体一起使用，避免活性成分失效；</ppingfang></spanlucida></ppingfang></ppingfang></ppingfang></ppingfang></ppingfang></ppingfang></ppingfang></ppingfang></ppingfang></p><p></p><p><ppingfang sc\';=\"\" #000000\"=\"\"><ppingfang sc\';=\"\" #000000\"=\"\"><ppingfang sc\';=\"\" #000000\"=\"\"><ppingfang sc\';=\"\" #000000\"=\"\"><ppingfang sc\';=\"\" #000000\"=\"\"><ppingfang sc\';=\"\" #000000\"=\"\"><ppingfang sc\';=\"\" #000000\"=\"\"><ppingfang sc\';=\"\" #000000\"=\"\"><ppingfang sc\';=\"\" #000000\"=\"\"><spanlucida grande\'\"=\"\"><ppingfang sc\';=\"\" #000000\"=\"\"><ppingfang sc\';=\"\" #000000\"=\"\">4.使用过程中如有不适，请立即停止使用。</ppingfang></ppingfang></spanlucida></ppingfang></ppingfang></ppingfang></ppingfang></ppingfang></ppingfang></ppingfang></ppingfang></ppingfang><img src=\"http://appserver.gxyhttest.com/Upload/adminec69158e-50d0-4bf1-a385-ee2b9f8b4403.jpg\" style=\"max-width: 100%;\"></p><p></p>', 'admin348895b9-61bd-4f0c-a725-47f32e7761fe.JPG', '2020-10-31 12:46:11', '1', '21', '大类2', '43', '食品', '', '0.00', '补水', '423', '0', '0', '0', '0', '0', '1');
INSERT INTO `db_shop_goods` VALUES ('68', '0052', '寡肽修护冻干粉（10ml×10组）', '0', '19700.00', '5.22', '9847', '3', '<p><img src=\"http://appserver.gxzzwl.com/Upload/admin8afaf394-2756-46a1-ba61-394106a21631.jpg\" style=\"max-width: 100%;\"></p><p></p>', 'adminac846d3c-f9c0-4567-b542-2336681d6647.JPG', '2021-01-18 15:18:23', '1', '20', '大类1', '40', '家电', '', '0.00', '补水', '11', '0', '0', '0', '0', '0', '1');

-- ----------------------------
-- Table structure for db_shop_goods_sort
-- ----------------------------
DROP TABLE IF EXISTS `db_shop_goods_sort`;
CREATE TABLE `db_shop_goods_sort` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `daleiname` varchar(50) DEFAULT NULL,
  `daleiorder` int(11) DEFAULT '0',
  `putaway` int(11) DEFAULT '0',
  `pagemark` int(11) DEFAULT '0',
  `pagemarklx` int(11) DEFAULT '0',
  `daleiimg` longtext,
  `adate` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP,
  PRIMARY KEY (`id`),
  UNIQUE KEY `id` (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=23 DEFAULT CHARSET=utf8mb4;

-- ----------------------------
-- Records of db_shop_goods_sort
-- ----------------------------
INSERT INTO `db_shop_goods_sort` VALUES ('1', '未分类', '1', '0', '0', '3', 'admindcb2cdff-fb68-4d89-a670-da7e842452e2.png', '2019-06-01 00:00:00');
INSERT INTO `db_shop_goods_sort` VALUES ('20', '大类1', '1', '1', '1', '1', 'admin2c4709db-9b1f-43c7-8d62-922eb63d571d.jpg', '2020-10-27 16:52:16');
INSERT INTO `db_shop_goods_sort` VALUES ('21', '大类2', '1', '1', '1', '1', '', '2020-10-27 19:00:47');
INSERT INTO `db_shop_goods_sort` VALUES ('22', '大类3', '1', '1', '0', '1', '', '2020-10-28 14:36:13');

-- ----------------------------
-- Table structure for db_shop_goods_sort_child
-- ----------------------------
DROP TABLE IF EXISTS `db_shop_goods_sort_child`;
CREATE TABLE `db_shop_goods_sort_child` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `sid` int(11) DEFAULT '0',
  `xiaoleiname` varchar(50) DEFAULT NULL,
  `xiaoleiorder` int(11) DEFAULT NULL,
  `pagemark` int(11) DEFAULT NULL,
  `xiaoleiimg` longtext,
  `adate` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `putaway` int(11) NOT NULL DEFAULT '0' COMMENT '上下架',
  PRIMARY KEY (`id`),
  KEY `sid` (`sid`),
  CONSTRAINT `db_shop_goods_sort_child_ibfk_1` FOREIGN KEY (`sid`) REFERENCES `db_shop_goods_sort` (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=49 DEFAULT CHARSET=utf8mb4;

-- ----------------------------
-- Records of db_shop_goods_sort_child
-- ----------------------------
INSERT INTO `db_shop_goods_sort_child` VALUES ('1', '1', '小类未分类', '1', '0', 'adminc9e14cfa-3dba-4b5a-a05b-d80cab8bca73.png', '2020-07-02 17:54:11', '1');
INSERT INTO `db_shop_goods_sort_child` VALUES ('40', '20', '家电', '1', '1', 'admind43c381c-9266-43c8-8c42-40049c603672.png', '2020-10-27 16:52:33', '1');
INSERT INTO `db_shop_goods_sort_child` VALUES ('41', '20', '妆品', '1', '1', 'admin26b2b680-fd88-4559-8943-213a8c3a399b.png', '2020-10-27 16:53:06', '1');
INSERT INTO `db_shop_goods_sort_child` VALUES ('42', '20', '服饰', '1', '1', 'adminff3e399b-4d92-4ea4-830e-8a4b1286c67a.png', '2020-10-27 19:01:52', '1');
INSERT INTO `db_shop_goods_sort_child` VALUES ('43', '21', '食品', '1', '1', 'admin44aec9d2-f916-4193-a10d-9eccab5744d1.png', '2020-10-27 19:02:21', '1');
INSERT INTO `db_shop_goods_sort_child` VALUES ('44', '22', '奥利奥', '1', '1', 'admin25ec94cd-a5f7-428f-ab05-3b055b7491ad.jpg', '2020-10-28 14:36:40', '1');
INSERT INTO `db_shop_goods_sort_child` VALUES ('45', '22', '油米', '1', '1', 'admin23550bef-7498-429f-9ab0-73c015bcdf9e.png', '2020-10-28 14:36:57', '1');
INSERT INTO `db_shop_goods_sort_child` VALUES ('46', '22', '酒水', '1', '1', 'admin3c3a05a7-1734-465d-98d2-69eb59462a46.jpg', '2020-10-28 14:37:03', '1');
INSERT INTO `db_shop_goods_sort_child` VALUES ('48', '21', '2222', '4', '1', 'admin07fe2594-499a-4c5c-a406-de3f8055d22e.png', '2021-03-10 15:26:27', '1');

-- ----------------------------
-- Table structure for db_shop_img
-- ----------------------------
DROP TABLE IF EXISTS `db_shop_img`;
CREATE TABLE `db_shop_img` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `img` longtext NOT NULL,
  `lx` int(11) NOT NULL DEFAULT '1' COMMENT '展示类型: 1单张.2三张',
  `bdlx` text NOT NULL,
  `gid` text NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8mb4;

-- ----------------------------
-- Records of db_shop_img
-- ----------------------------
INSERT INTO `db_shop_img` VALUES ('2', 'admindfb4e711-224f-42b4-9a89-c4129f93483b.jpg', '1', '2', '25');

-- ----------------------------
-- Table structure for db_shop_order
-- ----------------------------
DROP TABLE IF EXISTS `db_shop_order`;
CREATE TABLE `db_shop_order` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `bill_id` int(11) DEFAULT NULL,
  `orderNo` varchar(100) NOT NULL,
  `uid` int(11) NOT NULL DEFAULT '0',
  `userid` varchar(50) NOT NULL,
  `username` varchar(50) DEFAULT NULL,
  `usertel` varchar(50) DEFAULT NULL,
  `sheng` varchar(50) DEFAULT NULL,
  `shi` varchar(50) DEFAULT NULL,
  `xian` varchar(50) DEFAULT NULL,
  `useraddress` varchar(255) DEFAULT NULL,
  `sjine` decimal(18,2) NOT NULL DEFAULT '0.00' COMMENT '实价',
  `yjine` decimal(18,2) NOT NULL DEFAULT '0.00' COMMENT '原价',
  `pv` decimal(18,2) NOT NULL DEFAULT '0.00',
  `goodsnum` int(11) NOT NULL DEFAULT '0' COMMENT '商品总数',
  `goodslist` longtext,
  `odate` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `kuaidiname` varchar(50) DEFAULT NULL,
  `kuaidiNo` varchar(50) DEFAULT NULL,
  `fdate` datetime DEFAULT NULL,
  `beizhu` varchar(50) DEFAULT NULL,
  `cost` decimal(18,2) NOT NULL DEFAULT '0.00',
  `orderstate` int(11) NOT NULL DEFAULT '0',
  `isdelete` int(11) NOT NULL DEFAULT '0',
  `lx` int(11) NOT NULL DEFAULT '0',
  PRIMARY KEY (`id`),
  UNIQUE KEY `id` (`id`),
  KEY `uid` (`uid`),
  KEY `bill_id` (`bill_id`),
  CONSTRAINT `db_shop_order_ibfk_1` FOREIGN KEY (`uid`) REFERENCES `db_users` (`id`),
  CONSTRAINT `db_shop_order_ibfk_2` FOREIGN KEY (`bill_id`) REFERENCES `db_bill` (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=20 DEFAULT CHARSET=utf8mb4;

-- ----------------------------
-- Records of db_shop_order
-- ----------------------------
INSERT INTO `db_shop_order` VALUES ('18', '90', '20210728180828287820933323', '1', 'admin', '1', '12312313133', '北京市', '北京市', '东城区', '123', '0.00', '0.00', '115.00', '1', '[\r\n  {\r\n    \"Id\": 0,\r\n    \"OrderNo\": \"20210728180828287820933323\",\r\n    \"Oid\": 0,\r\n    \"Gid\": 66,\r\n    \"Goodsname\": \"寡肽修护冻干粉（3ml×10组）\",\r\n    \"Uid\": 1,\r\n    \"Userid\": \"admin\",\r\n    \"Sjine\": 0.0,\r\n    \"Yjine\": 0.0,\r\n    \"Num\": 1,\r\n    \"Zjine\": 0.0,\r\n    \"Goodsimg\": \"admin663fae26-bf76-41d3-8278-008ed300db78.JPG\",\r\n    \"Cdate\": \"2021-07-28 18:08:28\"\r\n  }\r\n]', '2021-07-28 18:18:02', '-', '-', '2021-08-26 10:44:20', '-', '0.00', '2', '0', '1');
INSERT INTO `db_shop_order` VALUES ('19', '93', '20210729101918191758616949', '1', 'admin', '1', '12312313133', '北京市', '北京市', '东城区', '123', '0.00', '0.00', '120.22', '2', '[\r\n  {\r\n    \"Id\": 0,\r\n    \"OrderNo\": \"20210729101918191758616949\",\r\n    \"Oid\": 0,\r\n    \"Gid\": 66,\r\n    \"Goodsname\": \"寡肽修护冻干粉（3ml×10组）\",\r\n    \"Uid\": 1,\r\n    \"Userid\": \"admin\",\r\n    \"Sjine\": 320.00,\r\n    \"Yjine\": 320.00,\r\n    \"Num\": 1,\r\n    \"Zjine\": 0.0,\r\n    \"Goodsimg\": \"admin663fae26-bf76-41d3-8278-008ed300db78.JPG\",\r\n    \"Cdate\": \"2021-07-29 10:19:18\"\r\n  },\r\n  {\r\n    \"Id\": 0,\r\n    \"OrderNo\": \"20210729101918191758616949\",\r\n    \"Oid\": 0,\r\n    \"Gid\": 68,\r\n    \"Goodsname\": \"寡肽修护冻干粉（10ml×10组）\",\r\n    \"Uid\": 1,\r\n    \"Userid\": \"admin\",\r\n    \"Sjine\": 19700.00,\r\n    \"Yjine\": 19700.00,\r\n    \"Num\": 1,\r\n    \"Zjine\": 0.0,\r\n    \"Goodsimg\": \"adminac846d3c-f9c0-4567-b542-2336681d6647.JPG\",\r\n    \"Cdate\": \"2021-07-29 10:19:18\"\r\n  }\r\n]', '2021-07-29 10:20:34', '-', '-', '2021-08-26 10:29:41', '-', '0.00', '3', '0', '1');

-- ----------------------------
-- Table structure for db_shop_order_child
-- ----------------------------
DROP TABLE IF EXISTS `db_shop_order_child`;
CREATE TABLE `db_shop_order_child` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `orderNo` varchar(100) DEFAULT NULL,
  `oid` int(11) NOT NULL DEFAULT '0',
  `gid` int(11) NOT NULL DEFAULT '0',
  `goodsname` varchar(255) DEFAULT NULL,
  `uid` int(11) NOT NULL DEFAULT '0',
  `userid` varchar(50) DEFAULT NULL,
  `sjine` decimal(18,2) NOT NULL DEFAULT '0.00',
  `yjine` decimal(18,2) NOT NULL DEFAULT '0.00',
  `num` int(11) NOT NULL DEFAULT '0',
  `zjine` decimal(18,2) NOT NULL DEFAULT '0.00',
  `goodsimg` longtext,
  `cdate` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP,
  PRIMARY KEY (`id`),
  KEY `oid` (`oid`),
  CONSTRAINT `db_shop_order_child_ibfk_1` FOREIGN KEY (`oid`) REFERENCES `db_shop_order` (`id`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=37 DEFAULT CHARSET=utf8mb4;

-- ----------------------------
-- Records of db_shop_order_child
-- ----------------------------
INSERT INTO `db_shop_order_child` VALUES ('34', '20210728180828287820933323', '18', '66', '寡肽修护冻干粉（3ml×10组）', '1', 'admin', '0.00', '0.00', '1', '0.00', 'admin663fae26-bf76-41d3-8278-008ed300db78.JPG', '2021-07-28 18:08:29');
INSERT INTO `db_shop_order_child` VALUES ('35', '20210729101918191758616949', '19', '66', '寡肽修护冻干粉（3ml×10组）', '1', 'admin', '320.00', '320.00', '1', '0.00', 'admin663fae26-bf76-41d3-8278-008ed300db78.JPG', '2021-07-29 10:19:19');
INSERT INTO `db_shop_order_child` VALUES ('36', '20210729101918191758616949', '19', '68', '寡肽修护冻干粉（10ml×10组）', '1', 'admin', '19700.00', '19700.00', '1', '0.00', 'adminac846d3c-f9c0-4567-b542-2336681d6647.JPG', '2021-07-29 10:19:19');

-- ----------------------------
-- Table structure for db_slide
-- ----------------------------
DROP TABLE IF EXISTS `db_slide`;
CREATE TABLE `db_slide` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `lx` int(11) NOT NULL DEFAULT '0',
  `gid` int(11) NOT NULL DEFAULT '0',
  `url` longtext NOT NULL,
  `img` longtext NOT NULL,
  `pagelx` int(11) NOT NULL DEFAULT '0',
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=50 DEFAULT CHARSET=utf8mb4;

-- ----------------------------
-- Records of db_slide
-- ----------------------------
INSERT INTO `db_slide` VALUES ('43', '2', '66', '/', 'admin79edd77b-47ee-410e-9dbf-106bbc718a87.jpg', '1');
INSERT INTO `db_slide` VALUES ('49', '1', '0', '/', 'admin93f62d79-6768-41b4-ba78-73a1ce720c00.jpg', '1');

-- ----------------------------
-- Table structure for db_system_achievement
-- ----------------------------
DROP TABLE IF EXISTS `db_system_achievement`;
CREATE TABLE `db_system_achievement` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `userscount` int(11) NOT NULL DEFAULT '0',
  `usersjine` decimal(18,2) NOT NULL DEFAULT '0.00',
  `bonus` decimal(18,2) NOT NULL DEFAULT '0.00',
  `ordercount` int(11) NOT NULL DEFAULT '0',
  `gouwucount` int(11) NOT NULL DEFAULT '0',
  `gouwujine` decimal(18,2) NOT NULL DEFAULT '0.00',
  `chongzhijine` decimal(18,2) NOT NULL DEFAULT '0.00',
  `tixianjine` decimal(18,2) NOT NULL DEFAULT '0.00',
  `adate` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=24 DEFAULT CHARSET=utf8mb4;

-- ----------------------------
-- Records of db_system_achievement
-- ----------------------------
INSERT INTO `db_system_achievement` VALUES ('7', '3', '798.00', '0.00', '0', '0', '0.00', '0.00', '0.00', '2021-07-23 16:20:20');
INSERT INTO `db_system_achievement` VALUES ('8', '0', '0.00', '0.00', '1', '1', '0.00', '0.00', '0.00', '2021-07-28 18:18:02');
INSERT INTO `db_system_achievement` VALUES ('9', '0', '0.00', '0.00', '1', '2', '0.00', '0.00', '0.00', '2021-07-29 10:20:34');
INSERT INTO `db_system_achievement` VALUES ('23', '0', '0.00', '0.00', '0', '0', '0.00', '0.00', '1313.00', '2021-08-26 10:43:14');

-- ----------------------------
-- Table structure for db_system_admin
-- ----------------------------
DROP TABLE IF EXISTS `db_system_admin`;
CREATE TABLE `db_system_admin` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `userid` varchar(50) DEFAULT NULL,
  `password` varchar(50) DEFAULT NULL,
  `username` varchar(50) DEFAULT NULL,
  `logintime` varchar(50) DEFAULT NULL,
  `loginip` varchar(50) DEFAULT NULL,
  `gid` int(11) DEFAULT NULL,
  `permissionname` varchar(50) DEFAULT NULL,
  `adate` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP,
  PRIMARY KEY (`id`),
  KEY `system_admin_ibfk_1` (`gid`),
  CONSTRAINT `db_system_admin_ibfk_1` FOREIGN KEY (`gid`) REFERENCES `db_system_admin_group` (`id`) ON DELETE SET NULL ON UPDATE SET NULL
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8mb4;

-- ----------------------------
-- Records of db_system_admin
-- ----------------------------
INSERT INTO `db_system_admin` VALUES ('1', 'admin', '21232f297a57a5a743894a0e4a801fc3', '总管理员', '2019-09-01 0:00:00', '1', null, '总管理员', '2019-09-01 00:00:00');

-- ----------------------------
-- Table structure for db_system_admin_group
-- ----------------------------
DROP TABLE IF EXISTS `db_system_admin_group`;
CREATE TABLE `db_system_admin_group` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `groupname` varchar(50) DEFAULT NULL,
  `permission` longtext,
  PRIMARY KEY (`id`),
  UNIQUE KEY `id` (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=42 DEFAULT CHARSET=utf8mb4;

-- ----------------------------
-- Records of db_system_admin_group
-- ----------------------------
INSERT INTO `db_system_admin_group` VALUES ('13', '总监', '[{\"Name\":\"用户管理\",\"Display\":true,\"Pclist\":[[{\"Name\":\"会员列表\",\"Path\":\"Users_AdminController/List\",\"Display\":true},{\"Name\":\"激活\",\"Path\":\"Users_AdminController/Jihuo\",\"Display\":true},{\"Name\":\"空单\",\"Path\":\"Users_AdminController/Kongdan\",\"Display\":true},{\"Name\":\"删除\",\"Path\":\"Users_AdminController/Delete\",\"Display\":true},{\"Name\":\"锁定\",\"Path\":\"Users_AdminController/Suoding\",\"Display\":true},{\"Name\":\"解锁\",\"Path\":\"Users_AdminController/Jiesuo\",\"Display\":true},{\"Name\":\"登录会员前台\",\"Path\":\"Users_AdminController/Login\",\"Display\":true}],[{\"Name\":\"会员资料\",\"Path\":\"Users_AdminController/Get\",\"Display\":true},{\"Name\":\"修改资料\",\"Path\":\"Users_AdminController/Update\",\"Display\":true},{\"Name\":\"修改密码\",\"Path\":\"Users_AdminController/Update_Password\",\"Display\":true},{\"Name\":\"修改推荐人\",\"Path\":\"Users_AdminController/Update_Rename\",\"Display\":true}],[{\"Name\":\"激活记录\",\"Path\":\"Fwzx_AdminController/Jihuo_Record_List\",\"Display\":true}],[{\"Name\":\"升级记录\",\"Path\":\"UsersUlevelup_AdminController/List\",\"Display\":true},{\"Name\":\"删除记录\",\"Path\":\"UsersUlevelup_AdminController/Delete\",\"Display\":true}],[{\"Name\":\"服务中心申请\",\"Path\":\"FwzxApply_AdminController/List\",\"Display\":true},{\"Name\":\"审核申请\",\"Path\":\"FwzxApply_AdminController/Pass\",\"Display\":true},{\"Name\":\"撤销申请\",\"Path\":\"FwzxApply_AdminController/Revoke\",\"Display\":true},{\"Name\":\"删除记录\",\"Path\":\"FwzxApply_AdminController/Delete\",\"Display\":true},{\"Name\":\"服务中心列表\",\"Path\":\"Fwzx_AdminController/List\",\"Display\":true},{\"Name\":\"撤销服务中心\",\"Path\":\"Fwzx_AdminController/Revoke\",\"Display\":true}]]},{\"Name\":\"奖金管理\",\"Display\":true,\"Pclist\":[[{\"Name\":\"分红结算\",\"Path\":\"JieSuan_AdminController/Record_List\",\"Display\":true},{\"Name\":\"奖金总表\",\"Path\":\"Bonus_AdminController/Time_List\",\"Display\":true},{\"Name\":\"奖金详情\",\"Path\":\"Bonus_AdminController/List\",\"Display\":true},{\"Name\":\"奖金来源\",\"Path\":\"Bonus_AdminController/Source_List\",\"Display\":true}]]},{\"Name\":\"财务管理\",\"Display\":true,\"Pclist\":[[{\"Name\":\"货币增减列表\",\"Path\":\"Zengjian_AdminController/List\",\"Display\":true},{\"Name\":\"货币增减\",\"Path\":\"Zengjian_AdminController/Add\",\"Display\":true},{\"Name\":\"货币查询\",\"Path\":\"Walletget_AdminController/wlletget\",\"Display\":true}],[{\"Name\":\"充值列表\",\"Path\":\"Chongzhi_AdminController/List\",\"Display\":true},{\"Name\":\"充值审核\",\"Path\":\"Chongzhi_AdminController/Pass\",\"Display\":true},{\"Name\":\"充值撤销\",\"Path\":\"Chongzhi_AdminController/Revoke\",\"Display\":true},{\"Name\":\"充值删除\",\"Path\":\"Chongzhi_AdminController/Delete\",\"Display\":true}],[{\"Name\":\"提现列表\",\"Path\":\"Tixian_AdminController/List\",\"Display\":true},{\"Name\":\"提现审核\",\"Path\":\"Tixian_AdminController/Pass\",\"Display\":true},{\"Name\":\"提现撤销\",\"Path\":\"Tixian_AdminController/Revoke\",\"Display\":true},{\"Name\":\"提现删除\",\"Path\":\"Tixian_AdminController/Delete\",\"Display\":true}],[{\"Name\":\"转换列表\",\"Path\":\"Zhuanhuan_AdminController/List\",\"Display\":true},{\"Name\":\"转换删除\",\"Path\":\"Zhuanhuan_AdminController/Delete\",\"Display\":true}],[{\"Name\":\"转账列表\",\"Path\":\"Zhuanzhang_AdminController/List\",\"Display\":true},{\"Name\":\"转账删除\",\"Path\":\"Zhuanzhang_AdminController/Delete\",\"Display\":true}]]},{\"Name\":\"商城管理\",\"Display\":true,\"Pclist\":[[{\"Name\":\"幻灯片列表\",\"Path\":\"Slide_AdminController/List\",\"Display\":true},{\"Name\":\"添加幻灯片\",\"Path\":\"Slide_AdminController/Add\",\"Display\":true},{\"Name\":\"查看幻灯片\",\"Path\":\"Slide_AdminController/Get\",\"Display\":true},{\"Name\":\"修改幻灯片\",\"Path\":\"Slide_AdminController/Update\",\"Display\":true},{\"Name\":\"删除幻灯片\",\"Path\":\"Slide_AdminController/Delete\",\"Display\":true}],[{\"Name\":\"商城图片列表\",\"Path\":\"ShopImg_AdminController/List\",\"Display\":true},{\"Name\":\"添加商城图片\",\"Path\":\"ShopImg_AdminController/Add\",\"Display\":true},{\"Name\":\"查看商城图片\",\"Path\":\"ShopImg_AdminController/Get\",\"Display\":true},{\"Name\":\"修改商城图片\",\"Path\":\"ShopImg_AdminController/Update\",\"Display\":true},{\"Name\":\"删除商城图片\",\"Path\":\"ShopImg_AdminController/Delete\",\"Display\":true}],[{\"Name\":\"大类列表\",\"Path\":\"GoodsSort_AdminController/List\",\"Display\":true},{\"Name\":\"添加大类\",\"Path\":\"GoodsSort_AdminController/Add\",\"Display\":true},{\"Name\":\"查看单个大类\",\"Path\":\"GoodsSort_AdminController/Get\",\"Display\":true},{\"Name\":\"修改大类\",\"Path\":\"GoodsSort_AdminController/Update\",\"Display\":true},{\"Name\":\"删除大类\",\"Path\":\"GoodsSort_AdminController/Delete\",\"Display\":true}],[{\"Name\":\"小类列表\",\"Path\":\"GoodsSortChild_AdminController/List\",\"Display\":true},{\"Name\":\"添加小类\",\"Path\":\"GoodsSortChild_AdminController/Add\",\"Display\":true},{\"Name\":\"修改小类\",\"Path\":\"GoodsSortChild_AdminController/Update\",\"Display\":true},{\"Name\":\"查看单个小类\",\"Path\":\"GoodsSortChild_AdminController/Get\",\"Display\":true},{\"Name\":\"删除小类\",\"Path\":\"GoodsSortChild_AdminController/Delete\",\"Display\":true}],[{\"Name\":\"商品列表\",\"Path\":\"Goods_AdminController/List\",\"Display\":true},{\"Name\":\"添加商品\",\"Path\":\"Goods_AdminController/Add\",\"Display\":true},{\"Name\":\"修改商品\",\"Path\":\"Goods_AdminController/Update\",\"Display\":true},{\"Name\":\"查看单个商品\",\"Path\":\"Goods_AdminController/Get\",\"Display\":true},{\"Name\":\"删除商品\",\"Path\":\"Goods_AdminController/Delete\",\"Display\":true}],[{\"Name\":\"订单列表\",\"Path\":\"Order_AdminController/List\",\"Display\":true},{\"Name\":\"查看订单详情\",\"Path\":\"Order_AdminController/Child_List\",\"Display\":true},{\"Name\":\"发货\",\"Path\":\"Order_AdminController/Pass\",\"Display\":true},{\"Name\":\"退货\",\"Path\":\"Order_AdminController/Revoke\",\"Display\":true},{\"Name\":\"删除\",\"Path\":\"Order_AdminController/Delete\",\"Display\":true}]]},{\"Name\":\"信息管理\",\"Display\":true,\"Pclist\":[[{\"Name\":\"客服消息列表\",\"Path\":\"Msg_AdminController/List_Chating\",\"Display\":true},{\"Name\":\"发送消息\",\"Path\":\"Msg_AdminController/Add\",\"Display\":true},{\"Name\":\"查询用户\",\"Path\":\"Msg_AdminController/List_First\",\"Display\":true},{\"Name\":\"新闻修改\",\"Path\":\"News_AdminController/Update\",\"Display\":true}],[{\"Name\":\"查看会员帮助\",\"Path\":\"Article_AdminController/Get\",\"Display\":true},{\"Name\":\"修改帮助\",\"Path\":\"Article_AdminController/Update\",\"Display\":true}],[{\"Name\":\"自动回复列表\",\"Path\":\"Help_AdminController/Question_List\",\"Display\":true},{\"Name\":\"添加问题回复\",\"Path\":\"Help_AdminController/Add\",\"Display\":true},{\"Name\":\"修改问题回复\",\"Path\":\"Help_AdminController/Update\",\"Display\":true},{\"Name\":\"删除问题回复\",\"Path\":\"Help_AdminController/Delete\",\"Display\":true}],[{\"Name\":\"新闻列表\",\"Path\":\"News_AdminController/List\",\"Display\":true},{\"Name\":\"新闻添加\",\"Path\":\"News_AdminController/Add\",\"Display\":true},{\"Name\":\"新闻删除\",\"Path\":\"News_AdminController/Delete\",\"Display\":true},{\"Name\":\"查看单个修改\",\"Path\":\"News_AdminController/Get\",\"Display\":true},{\"Name\":\"新闻修改\",\"Path\":\"News_AdminController/Update\",\"Display\":true}],[{\"Name\":\"业绩统计\",\"Path\":\"Achievement_AdminController/List\",\"Display\":true},{\"Name\":\"登录日志\",\"Path\":\"Log_AdminController/Login_List\",\"Display\":true},{\"Name\":\"操作日志\",\"Path\":\"Log_AdminController/Operation_List\",\"Display\":true},{\"Name\":\"删除操作日志\",\"Path\":\"Log_AdminController/Operation_Delete\",\"Display\":true},{\"Name\":\"异常日志\",\"Path\":\"Log_AdminController/Error_List\",\"Display\":true},{\"Name\":\"删除异常日志\",\"Path\":\"Log_AdminController/Error_Delect\",\"Display\":true}]]}]');
INSERT INTO `db_system_admin_group` VALUES ('41', '测试1', '[{\"Name\":\"用户管理\",\"Display\":true,\"Pclist\":[[{\"Name\":\"会员列表\",\"Path\":\"Users_AdminController/List\",\"Display\":true},{\"Name\":\"激活\",\"Path\":\"Users_AdminController/Jihuo\",\"Display\":true},{\"Name\":\"空单\",\"Path\":\"Users_AdminController/Kongdan\",\"Display\":true},{\"Name\":\"删除\",\"Path\":\"Users_AdminController/Delete\",\"Display\":true},{\"Name\":\"锁定\",\"Path\":\"Users_AdminController/Suoding\",\"Display\":true},{\"Name\":\"解锁\",\"Path\":\"Users_AdminController/Jiesuo\",\"Display\":true},{\"Name\":\"登录会员前台\",\"Path\":\"Users_AdminController/Login\",\"Display\":true}],[{\"Name\":\"会员资料\",\"Path\":\"Users_AdminController/Get\",\"Display\":true},{\"Name\":\"修改资料\",\"Path\":\"Users_AdminController/Update\",\"Display\":true},{\"Name\":\"修改密码\",\"Path\":\"Users_AdminController/Update_Password\",\"Display\":true},{\"Name\":\"修改推荐人\",\"Path\":\"Users_AdminController/Update_Rename\",\"Display\":true},{\"Name\":\"会员业绩\",\"Path\":\"Users_AdminController/QueryPerformance\",\"Display\":true}],[{\"Name\":\"网络图谱\",\"Path\":\"Users_AdminController/Network\",\"Display\":true}],[{\"Name\":\"推荐图谱\",\"Path\":\"Users_AdminController/Tree\",\"Display\":true}],[{\"Name\":\"激活记录\",\"Path\":\"UsersJihuoRecord_AdminController/List\",\"Display\":true}],[{\"Name\":\"升级记录\",\"Path\":\"UsersLevelup_AdminController/List\",\"Display\":true},{\"Name\":\"删除记录\",\"Path\":\"UsersLevelup_AdminController/Delete\",\"Display\":true}],[{\"Name\":\"服务中心申请\",\"Path\":\"UsersFwzxApply_AdminController/List\",\"Display\":true},{\"Name\":\"审核申请\",\"Path\":\"UsersFwzxApply_AdminController/Pass\",\"Display\":true},{\"Name\":\"撤销申请\",\"Path\":\"UsersFwzxApply_AdminController/Revoke\",\"Display\":true},{\"Name\":\"删除记录\",\"Path\":\"UsersFwzxApply_AdminController/Delete\",\"Display\":true},{\"Name\":\"服务中心列表\",\"Path\":\"Users_AdminController/ListFwzx\",\"Display\":true},{\"Name\":\"撤销服务中心\",\"Path\":\"Users_AdminController/RevokeFwzx\",\"Display\":true}]]},{\"Name\":\"奖金管理\",\"Display\":true,\"Pclist\":[[{\"Name\":\"分红列表\",\"Path\":\"BonusJiesuan_AdminController/List\",\"Display\":true},{\"Name\":\"分红结算\",\"Path\":\"BonusJiesuan_AdminController/Jiesuan\",\"Display\":true},{\"Name\":\"奖金总表\",\"Path\":\"Bonus_AdminController/Time_List\",\"Display\":true},{\"Name\":\"奖金详情\",\"Path\":\"Bonus_AdminController/List\",\"Display\":true},{\"Name\":\"奖金来源\",\"Path\":\"Bonus_AdminController/Source_List\",\"Display\":true}]]},{\"Name\":\"财务管理\",\"Display\":true,\"Pclist\":[[{\"Name\":\"货币管理\",\"Path\":\"WalletsCoin_AdminController/List\",\"Display\":true},{\"Name\":\"添加货币\",\"Path\":\"WalletsCoin_AdminController/Add\",\"Display\":true},{\"Name\":\"删除货币\",\"Path\":\"WalletsCoin_AdminController/Delete\",\"Display\":true},{\"Name\":\"货币启用\",\"Path\":\"WalletsCoin_AdminController/ChangeState\",\"Display\":true},{\"Name\":\"修改英文名称\",\"Path\":\"WalletsCoin_AdminController/ChangeCoinName\",\"Display\":true},{\"Name\":\"修改中文名称\",\"Path\":\"WalletsCoin_AdminController/ChangeCoinNameZh\",\"Display\":true}],[{\"Name\":\"充值货币\",\"Path\":\"WalletsChongzhi_Select_AdminController/List\",\"Display\":true},{\"Name\":\"添加充值货币\",\"Path\":\"WalletsChongzhi_Select_AdminController/Add\",\"Display\":true},{\"Name\":\"修改充值货币\",\"Path\":\"WalletsChongzhi_Select_AdminController/Update\",\"Display\":true},{\"Name\":\"删除充值货币\",\"Path\":\"WalletsChongzhi_Select_AdminController/Delete\",\"Display\":true}],[{\"Name\":\"提现货币\",\"Path\":\"WalletsTixian_Select_AdminController/List\",\"Display\":true},{\"Name\":\"添加提现货币\",\"Path\":\"WalletsTixian_Select_AdminController/Add\",\"Display\":true},{\"Name\":\"修改提现货币\",\"Path\":\"WalletsTixian_Select_AdminController/Update\",\"Display\":true},{\"Name\":\"删除提现货币\",\"Path\":\"WalletsTixian_Select_AdminController/Delete\",\"Display\":true}],[{\"Name\":\"转账货币\",\"Path\":\"WalletsZhuanzhang_Select_AdminController/List\",\"Display\":true},{\"Name\":\"添加转账货币\",\"Path\":\"WalletsZhuanzhang_Select_AdminController/Add\",\"Display\":true},{\"Name\":\"修改转账货币\",\"Path\":\"WalletsZhuanzhang_Select_AdminController/Update\",\"Display\":true},{\"Name\":\"删除转账货币\",\"Path\":\"WalletsZhuanzhang_Select_AdminController/Delete\",\"Display\":true},{\"Name\":\"团队限制\",\"Path\":\"WalletsZhuanzhang_Select_AdminController/IsState\",\"Display\":true}],[{\"Name\":\"转换货币\",\"Path\":\"WalletsZhuanhuan_Select_AdminController/List\",\"Display\":true},{\"Name\":\"添加转换货币\",\"Path\":\"WalletsZhuanhuan_Select_AdminController/Add\",\"Display\":true},{\"Name\":\"修改转换货币\",\"Path\":\"WalletsZhuanhuan_Select_AdminController/Update\",\"Display\":true},{\"Name\":\"删除转换货币\",\"Path\":\"WalletsZhuanhuan_Select_AdminController/Delete\",\"Display\":true}],[{\"Name\":\"货币增减列表\",\"Path\":\"WalletsZengjian_AdminController/List\",\"Display\":true},{\"Name\":\"货币增减\",\"Path\":\"WalletsZengjian_AdminController/Zengjian\",\"Display\":true},{\"Name\":\"删除增减记录\",\"Path\":\"WalletsZengjian_AdminController/Delete\",\"Display\":true},{\"Name\":\"货币查询\",\"Path\":\"Wallets_AdminController/GetWallet\",\"Display\":true}],[{\"Name\":\"充值列表\",\"Path\":\"WalletsChongzhi_AdminController/List\",\"Display\":true},{\"Name\":\"充值审核\",\"Path\":\"WalletsChongzhi_AdminController/Pass\",\"Display\":true},{\"Name\":\"充值撤销\",\"Path\":\"WalletsChongzhi_AdminController/Revoke\",\"Display\":true},{\"Name\":\"充值删除\",\"Path\":\"WalletsChongzhi_AdminController/Delete\",\"Display\":true}],[{\"Name\":\"提现列表\",\"Path\":\"WalletsTixian_AdminController/List\",\"Display\":true},{\"Name\":\"提现审核\",\"Path\":\"WalletsTixian_AdminController/Pass\",\"Display\":true},{\"Name\":\"提现撤销\",\"Path\":\"WalletsTixian_AdminController/Revoke\",\"Display\":true},{\"Name\":\"提现删除\",\"Path\":\"WalletsTixian_AdminController/Delete\",\"Display\":true}],[{\"Name\":\"转换列表\",\"Path\":\"WalletsZhuanhuan_AdminController/List\",\"Display\":true},{\"Name\":\"转换删除\",\"Path\":\"WalletsZhuanhuan_AdminController/Delete\",\"Display\":true}],[{\"Name\":\"转账列表\",\"Path\":\"WalletsZhuanzhang_AdminController/List\",\"Display\":true},{\"Name\":\"转账删除\",\"Path\":\"WalletsZhuanzhang_AdminController/Delete\",\"Display\":true}]]},{\"Name\":\"商城管理\",\"Display\":true,\"Pclist\":[[{\"Name\":\"幻灯片列表\",\"Path\":\"Slide_AdminController/List\",\"Display\":true},{\"Name\":\"添加幻灯片\",\"Path\":\"Slide_AdminController/Add\",\"Display\":true},{\"Name\":\"查看幻灯片\",\"Path\":\"Slide_AdminController/Get\",\"Display\":true},{\"Name\":\"修改幻灯片\",\"Path\":\"Slide_AdminController/Update\",\"Display\":true},{\"Name\":\"删除幻灯片\",\"Path\":\"Slide_AdminController/Delete\",\"Display\":true}],[{\"Name\":\"商城图片列表\",\"Path\":\"ShopImg_AdminController/List\",\"Display\":true},{\"Name\":\"添加商城图片\",\"Path\":\"ShopImg_AdminController/Add\",\"Display\":true},{\"Name\":\"查看商城图片\",\"Path\":\"ShopImg_AdminController/Get\",\"Display\":true},{\"Name\":\"修改商城图片\",\"Path\":\"ShopImg_AdminController/Update\",\"Display\":true},{\"Name\":\"删除商城图片\",\"Path\":\"ShopImg_AdminController/Delete\",\"Display\":true}],[{\"Name\":\"大类列表\",\"Path\":\"ShopGoodsSort_AdminController/List\",\"Display\":true},{\"Name\":\"添加大类\",\"Path\":\"ShopGoodsSort_AdminController/Add\",\"Display\":true},{\"Name\":\"查看单个大类\",\"Path\":\"ShopGoodsSort_AdminController/Get\",\"Display\":true},{\"Name\":\"修改大类\",\"Path\":\"ShopGoodsSort_AdminController/Update\",\"Display\":true},{\"Name\":\"删除大类\",\"Path\":\"ShopGoodsSort_AdminController/Delete\",\"Display\":true}],[{\"Name\":\"小类列表\",\"Path\":\"ShopGoodsSortChild_AdminController/List\",\"Display\":true},{\"Name\":\"添加小类\",\"Path\":\"ShopGoodsSortChild_AdminController/Add\",\"Display\":true},{\"Name\":\"修改小类\",\"Path\":\"ShopGoodsSortChild_AdminController/Update\",\"Display\":true},{\"Name\":\"查看单个小类\",\"Path\":\"ShopGoodsSortChild_AdminController/Get\",\"Display\":true},{\"Name\":\"删除小类\",\"Path\":\"ShopGoodsSortChild_AdminController/Delete\",\"Display\":true}],[{\"Name\":\"商品列表\",\"Path\":\"ShopGoods_AdminController/List\",\"Display\":true},{\"Name\":\"添加商品\",\"Path\":\"ShopGoods_AdminController/Add\",\"Display\":true},{\"Name\":\"修改商品\",\"Path\":\"ShopGoods_AdminController/Update\",\"Display\":true},{\"Name\":\"查看单个商品\",\"Path\":\"ShopGoods_AdminController/Get\",\"Display\":true},{\"Name\":\"删除商品\",\"Path\":\"ShopGoods_AdminController/Delete\",\"Display\":true}],[{\"Name\":\"订单列表\",\"Path\":\"ShopOrder_AdminController/List\",\"Display\":true},{\"Name\":\"查看订单详情\",\"Path\":\"ShopOrder_AdminController/Child_List\",\"Display\":true},{\"Name\":\"发货\",\"Path\":\"ShopOrder_AdminController/Pass\",\"Display\":true},{\"Name\":\"退货\",\"Path\":\"ShopOrder_AdminController/Revoke\",\"Display\":true},{\"Name\":\"删除\",\"Path\":\"ShopOrder_AdminController/Delete\",\"Display\":true}]]},{\"Name\":\"信息管理\",\"Display\":true,\"Pclist\":[[{\"Name\":\"客服消息列表\",\"Path\":\"Msg_AdminController/List_Chating\",\"Display\":true},{\"Name\":\"发送消息\",\"Path\":\"Msg_AdminController/Add\",\"Display\":true},{\"Name\":\"获取聊天记录\",\"Path\":\"Msg_AdminController/List_First\",\"Display\":true},{\"Name\":\"获取用户\",\"Path\":\"Msg_AdminController/List_User\",\"Display\":true},{\"Name\":\"获取端口号\",\"Path\":\"WebSocket_AdminController/GetPort\",\"Display\":true}],[{\"Name\":\"查看会员帮助\",\"Path\":\"Article_AdminController/Get\",\"Display\":true},{\"Name\":\"修改帮助\",\"Path\":\"Article_AdminController/Update\",\"Display\":true}],[{\"Name\":\"自动回复列表\",\"Path\":\"Help_AdminController/Question_List\",\"Display\":true},{\"Name\":\"添加问题回复\",\"Path\":\"Help_AdminController/Add\",\"Display\":true},{\"Name\":\"修改问题回复\",\"Path\":\"Help_AdminController/Update\",\"Display\":true},{\"Name\":\"排序\",\"Path\":\"Help_AdminController/ChangeRank\",\"Display\":true},{\"Name\":\"是否显示\",\"Path\":\"Help_AdminController/ChangeShow\",\"Display\":true},{\"Name\":\"删除问题回复\",\"Path\":\"Help_AdminController/Delete\",\"Display\":true}],[{\"Name\":\"新闻列表\",\"Path\":\"News_AdminController/List\",\"Display\":true},{\"Name\":\"新闻添加\",\"Path\":\"News_AdminController/Add\",\"Display\":true},{\"Name\":\"新闻删除\",\"Path\":\"News_AdminController/Delete\",\"Display\":true},{\"Name\":\"查看单个修改\",\"Path\":\"News_AdminController/Get\",\"Display\":true},{\"Name\":\"新闻修改\",\"Path\":\"News_AdminController/Update\",\"Display\":true}],[{\"Name\":\"业绩统计\",\"Path\":\"SystemAchievement_AdminController/List\",\"Display\":true},{\"Name\":\"登录日志\",\"Path\":\"SystemLogLogin_AdminController/Login_List\",\"Display\":true},{\"Name\":\"操作日志\",\"Path\":\"SystemLogOperation_AdminController/Operation_List\",\"Display\":true},{\"Name\":\"删除操作日志\",\"Path\":\"SystemLogOperation_AdminController/Operation_Delete\",\"Display\":true},{\"Name\":\"异常日志\",\"Path\":\"SystemLogError_AdminController/Error_List\",\"Display\":true},{\"Name\":\"删除异常日志\",\"Path\":\"SystemLogError_AdminController/Error_Delete\",\"Display\":true}]]}]');

-- ----------------------------
-- Table structure for db_system_log
-- ----------------------------
DROP TABLE IF EXISTS `db_system_log`;
CREATE TABLE `db_system_log` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `userid` varchar(50) DEFAULT NULL COMMENT '管理员userid',
  `username` varchar(50) DEFAULT NULL COMMENT '管理员姓名',
  `ip` varchar(50) DEFAULT NULL COMMENT '操作人IP地址',
  `lx` int(11) NOT NULL DEFAULT '0' COMMENT '操作类型',
  `lx_name` varchar(255) DEFAULT NULL COMMENT '操作类型名称',
  `bz` longtext COMMENT '备注',
  `ldate` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP COMMENT '操作日期',
  `old_data` longtext COMMENT '旧数据json',
  `new_data` longtext COMMENT '新数据json',
  `is_del` int(11) NOT NULL DEFAULT '0' COMMENT '是否删除',
  `uid` int(11) DEFAULT NULL COMMENT '关联的会员uid',
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=1945 DEFAULT CHARSET=utf8mb4;

-- ----------------------------
-- Records of db_system_log
-- ----------------------------
INSERT INTO `db_system_log` VALUES ('1896', 'admin', null, '::1', '10', null, '清空数据库', '2021-07-23 16:20:00', null, null, '0', null);
INSERT INTO `db_system_log` VALUES ('1897', 'admin', null, '::1', '1', null, '激活会员:aaa', '2021-07-23 16:20:23', null, null, '0', null);
INSERT INTO `db_system_log` VALUES ('1898', 'admin', null, '::1', '1', null, '激活会员:bbb', '2021-07-23 16:21:25', null, null, '0', null);
INSERT INTO `db_system_log` VALUES ('1899', 'admin', null, '::1', '1', null, '激活会员:ccc', '2021-07-23 16:21:28', null, null, '0', null);
INSERT INTO `db_system_log` VALUES ('1900', 'admin', '总管理员', '::1', '0', '登录', null, '2021-07-26 21:57:50', null, null, '0', null);
INSERT INTO `db_system_log` VALUES ('1901', 'admin', null, '::1', '2', '充值', '审核充值', '2021-07-28 16:32:03', null, null, '0', null);
INSERT INTO `db_system_log` VALUES ('1902', 'admin', null, '::1', '9', '系统', '还原数据:20210729145714', '2021-07-29 15:08:12', null, null, '0', null);
INSERT INTO `db_system_log` VALUES ('1903', 'admin', null, '::1', '2', '充值', '审核充值', '2021-07-29 15:08:18', null, null, '0', null);
INSERT INTO `db_system_log` VALUES ('1904', 'admin', null, '::1', '2', '充值', '审核充值', '2021-07-29 15:08:25', null, null, '0', null);
INSERT INTO `db_system_log` VALUES ('1905', 'admin', null, '::1', '7', '商城', '修改商品:寡肽修护冻干粉（3ml×10组）', '2021-07-30 14:29:11', null, null, '0', null);
INSERT INTO `db_system_log` VALUES ('1906', 'admin', null, '::1', '7', '商城', '修改商品:寡肽修护冻干粉（3ml×10组）', '2021-07-30 14:29:54', null, null, '0', null);
INSERT INTO `db_system_log` VALUES ('1907', 'admin', null, '::1', '7', '商城', '修改商品:寡肽修护冻干粉（3ml×10组）', '2021-07-30 15:08:23', null, null, '0', null);
INSERT INTO `db_system_log` VALUES ('1908', 'admin', null, '::1', '7', '商城', '修改商品:寡肽修护冻干粉（3ml×10组）', '2021-07-30 15:08:25', null, null, '0', null);
INSERT INTO `db_system_log` VALUES ('1909', 'admin', null, '::1', '7', '商城', '修改商品:寡肽修护冻干粉（3ml×30组）', '2021-07-30 15:08:27', null, null, '0', null);
INSERT INTO `db_system_log` VALUES ('1910', 'admin', null, '::1', '7', '商城', '修改商品:寡肽修护冻干粉（3ml×10组）', '2021-07-30 16:09:28', null, null, '0', null);
INSERT INTO `db_system_log` VALUES ('1911', 'admin', null, '::1', '7', '商城', '修改商品:寡肽修护冻干粉（3ml×30组）', '2021-07-30 16:09:28', null, null, '0', null);
INSERT INTO `db_system_log` VALUES ('1912', 'admin', null, '::1', '7', '商城', '修改商品:寡肽修护冻干粉（3ml×30组）', '2021-07-30 16:09:31', null, null, '0', null);
INSERT INTO `db_system_log` VALUES ('1913', 'admin', null, '::1', '7', '商城', '修改商品:寡肽修护冻干粉（10ml×10组）', '2021-07-30 16:18:11', null, null, '0', null);
INSERT INTO `db_system_log` VALUES ('1914', 'admin', null, '::1', '11', '其他操作', '修改文章:提现帮助', '2021-07-30 17:47:25', null, null, '0', null);
INSERT INTO `db_system_log` VALUES ('1915', 'admin', null, '::1', '1', '会员', '修改会员资料:ccc', '2021-08-01 06:46:20', null, null, '0', '13');
INSERT INTO `db_system_log` VALUES ('1916', 'admin', '总管理员', '::1', '0', '登录', null, '2021-08-01 06:55:53', null, null, '0', null);
INSERT INTO `db_system_log` VALUES ('1917', 'admin', null, '::1', '9', '系统', '添加奖金参数', '2021-08-06 16:41:04', null, null, '0', null);
INSERT INTO `db_system_log` VALUES ('1918', 'admin', null, '::1', '9', '系统', '删除奖金参数', '2021-08-06 16:41:09', null, null, '0', null);
INSERT INTO `db_system_log` VALUES ('1919', 'admin', null, '::1', '3', '提现', '审核提现:1', '2021-08-09 11:34:07', null, null, '0', null);
INSERT INTO `db_system_log` VALUES ('1920', 'admin', null, '::1', '3', '提现', '审核提现:管理员', '2021-08-09 11:34:13', null, null, '0', null);
INSERT INTO `db_system_log` VALUES ('1921', 'admin', null, '::1', '3', '提现', '审核提现:1', '2021-08-09 11:35:09', null, null, '0', null);
INSERT INTO `db_system_log` VALUES ('1922', 'admin', null, '::1', '3', '提现', '审核提现:1', '2021-08-09 11:35:25', null, null, '0', null);
INSERT INTO `db_system_log` VALUES ('1923', 'admin', null, '::1', '3', '提现', '审核提现:1', '2021-08-09 11:44:26', null, null, '0', null);
INSERT INTO `db_system_log` VALUES ('1924', 'admin', null, '::1', '3', '提现', '审核提现:1', '2021-08-26 09:32:55', null, null, '0', null);
INSERT INTO `db_system_log` VALUES ('1925', 'admin', null, '::1', '1', '会员', '锁定会员:ccc', '2021-08-26 09:34:16', null, null, '0', null);
INSERT INTO `db_system_log` VALUES ('1926', 'admin', null, '::1', '1', '会员', '解锁会员:ccc', '2021-08-26 09:50:09', null, null, '0', null);
INSERT INTO `db_system_log` VALUES ('1927', 'admin', null, '::1', '3', '提现', '审核提现:1', '2021-08-26 09:53:01', null, null, '0', null);
INSERT INTO `db_system_log` VALUES ('1928', 'admin', null, '::1', '3', '提现', '审核提现:1', '2021-08-26 09:58:40', null, null, '0', null);
INSERT INTO `db_system_log` VALUES ('1929', 'admin', null, '::1', '3', '提现', '审核提现:1', '2021-08-26 10:00:33', null, null, '0', null);
INSERT INTO `db_system_log` VALUES ('1930', 'admin', null, '::1', '3', '提现', '审核提现:1', '2021-08-26 10:05:36', null, null, '0', null);
INSERT INTO `db_system_log` VALUES ('1931', 'admin', null, '::1', '3', '提现', '审核提现:1', '2021-08-26 10:14:10', null, null, '0', null);
INSERT INTO `db_system_log` VALUES ('1932', 'admin', null, '::1', '3', '提现', '审核提现:1', '2021-08-26 10:17:21', null, null, '0', null);
INSERT INTO `db_system_log` VALUES ('1933', 'admin', null, '::1', '3', '提现', '审核提现:1', '2021-08-26 10:21:06', null, null, '0', null);
INSERT INTO `db_system_log` VALUES ('1934', 'admin', null, '::1', '3', '提现', '审核提现:1', '2021-08-26 10:26:28', null, null, '0', null);
INSERT INTO `db_system_log` VALUES ('1935', 'admin', null, '::1', '8', '订单', '订单发货:1', '2021-08-26 10:29:42', null, null, '0', null);
INSERT INTO `db_system_log` VALUES ('1936', 'admin', null, '::1', '3', '提现', '审核提现:1', '2021-08-26 10:30:52', null, null, '0', null);
INSERT INTO `db_system_log` VALUES ('1937', 'admin', null, '::1', '3', '提现', '审核提现:1', '2021-08-26 10:32:28', null, null, '0', null);
INSERT INTO `db_system_log` VALUES ('1938', 'admin', null, '::1', '3', '提现', '审核提现:1', '2021-08-26 10:35:36', null, null, '0', null);
INSERT INTO `db_system_log` VALUES ('1939', 'admin', null, '::1', '3', '提现', '审核提现:1', '2021-08-26 10:37:21', null, null, '0', null);
INSERT INTO `db_system_log` VALUES ('1940', 'admin', null, '::1', '3', '提现', '审核提现:管理员', '2021-08-26 10:37:22', null, null, '0', null);
INSERT INTO `db_system_log` VALUES ('1941', 'admin', null, '::1', '3', '提现', '审核提现:1', '2021-08-26 10:43:13', null, null, '0', null);
INSERT INTO `db_system_log` VALUES ('1942', 'admin', null, '::1', '3', '提现', '审核提现:管理员', '2021-08-26 10:43:13', null, null, '0', null);
INSERT INTO `db_system_log` VALUES ('1943', 'admin', null, '::1', '8', '订单', '订单发货:1', '2021-08-26 10:44:20', null, null, '0', null);
INSERT INTO `db_system_log` VALUES ('1944', 'admin', null, '::1', '3', '提现', '审核提现:管理员', '2021-08-26 10:47:22', null, null, '0', null);

-- ----------------------------
-- Table structure for db_system_setting
-- ----------------------------
DROP TABLE IF EXISTS `db_system_setting`;
CREATE TABLE `db_system_setting` (
  `id` int(11) NOT NULL,
  `marqueemsg` longtext NOT NULL,
  `switchsystem` int(11) NOT NULL DEFAULT '0',
  `systemclosemsg` longtext NOT NULL,
  `timestart` int(11) DEFAULT NULL,
  `timeend` int(11) DEFAULT NULL,
  `timeclosemsg` longtext NOT NULL,
  `switchtjt` int(11) NOT NULL DEFAULT '0',
  `switchwlt` int(11) NOT NULL DEFAULT '0',
  `switchchongzhi` int(11) NOT NULL DEFAULT '0',
  `switchtixian` int(11) NOT NULL DEFAULT '0',
  `switchzhuanzhang` int(11) NOT NULL DEFAULT '0',
  `switchzhuanhuan` int(11) NOT NULL DEFAULT '0',
  `bank` longtext NOT NULL,
  `txmin` decimal(18,2) NOT NULL DEFAULT '0.00',
  `txmax` decimal(18,2) NOT NULL DEFAULT '0.00',
  `txbs` int(11) NOT NULL DEFAULT '0',
  `txsl` decimal(18,2) NOT NULL DEFAULT '0.00',
  `czmin` decimal(18,2) NOT NULL DEFAULT '0.00',
  `czmax` decimal(18,2) NOT NULL DEFAULT '0.00',
  `czbs` int(11) NOT NULL DEFAULT '0',
  `zzmin` decimal(18,2) NOT NULL DEFAULT '0.00',
  `zzmax` decimal(18,2) NOT NULL DEFAULT '0.00',
  `zzbs` int(11) NOT NULL DEFAULT '0',
  `zhbs` int(11) NOT NULL DEFAULT '0',
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- ----------------------------
-- Records of db_system_setting
-- ----------------------------
INSERT INTO `db_system_setting` VALUES ('1', '欢迎使用会员管理系统 欢迎使用会员管理系统 欢迎使用会员管理系统', '1', '系统正在维护,请等待开放', '0', '23', '系统开放时段为0点-23点', '1', '1', '1', '1', '1', '1', '支付宝,微信,农业银行,工商银行,建设银行,中国银行', '100.00', '9999999999.00', '100', '5.00', '1.00', '9999999999.00', '1', '1.00', '9999999999.00', '1', '1');

-- ----------------------------
-- Table structure for db_system_setting_bonus
-- ----------------------------
DROP TABLE IF EXISTS `db_system_setting_bonus`;
CREATE TABLE `db_system_setting_bonus` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `code` varchar(100) NOT NULL COMMENT '代号',
  `name` varchar(255) NOT NULL COMMENT '名称',
  `value` decimal(18,2) NOT NULL DEFAULT '0.00' COMMENT '参数',
  `bz` varchar(255) DEFAULT NULL,
  `index` int(11) NOT NULL COMMENT '排序',
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8mb4;

-- ----------------------------
-- Records of db_system_setting_bonus
-- ----------------------------
INSERT INTO `db_system_setting_bonus` VALUES ('1', 'lsk1', '会员1', '100.00', null, '1');
INSERT INTO `db_system_setting_bonus` VALUES ('2', 'lsk2', '会员2', '200.00', '', '2');
INSERT INTO `db_system_setting_bonus` VALUES ('3', 'lsk3', '会员3', '300.00', null, '3');

-- ----------------------------
-- Table structure for db_token
-- ----------------------------
DROP TABLE IF EXISTS `db_token`;
CREATE TABLE `db_token` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `uid` int(11) NOT NULL DEFAULT '0',
  `userid` varchar(50) NOT NULL,
  `tokenstr` longtext NOT NULL,
  `ip` varchar(50) DEFAULT NULL,
  `os` longtext,
  `odate` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `isa` int(11) NOT NULL DEFAULT '0',
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=3157 DEFAULT CHARSET=utf8mb4;

-- ----------------------------
-- Records of db_token
-- ----------------------------
INSERT INTO `db_token` VALUES ('2089', '1', 'admin', '9zmG7wuq54l0y+7JRWlFPKeWzpTcqiwN1QhSSKg4QqkAb4C8BmxxySB+u3h/rHQ/gK6X+GHLanVa8ydTmF9ZD/wtD9KuJyfvQFKP+aPryfXQZS81GpNOH5mFiFj/Rvi8xy8ZJxgXfRJ9KZX28svbIOMnSPOCNTLqdu+IkOP5O0XFdgrORZhDdq0sQywuwVgzbS/T3uje2pDXDWOO/Hjipg==', '::1', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/83.0.4103.97 Safari/537.36', '2020-08-08 11:50:41', '1');
INSERT INTO `db_token` VALUES ('2090', '1', 'admin', '9zmG7wuq54l0y+7JRWlFPKeWzpTcqiwN1QhSSKg4QqkAb4C8BmxxySB+u3h/rHQ/gK6X+GHLanVa8ydTmF9ZD/wtD9KuJyfvQFKP+aPryfXQZS81GpNOH5mFiFj/Rvi8xy8ZJxgXfRJ9KZX28svbIEIXXVjxqzWduWZVx0ijT3or9OQKtEh8vR8MIPSrafh6Skcb2lvlAJbrRvHJQ0b5EA==', '::1', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/84.0.4147.105 Safari/537.36', '2020-08-08 15:57:52', '1');
INSERT INTO `db_token` VALUES ('2091', '1', 'admin', '9zmG7wuq54l0y+7JRWlFPKeWzpTcqiwN1QhSSKg4QqkAb4C8BmxxySB+u3h/rHQ/gK6X+GHLanVa8ydTmF9ZD/wtD9KuJyfvQFKP+aPryfXQZS81GpNOH5mFiFj/Rvi8xy8ZJxgXfRJ9KZX28svbIEIXXVjxqzWduWZVx0ijT3or9OQKtEh8vR8MIPSrafh6Skcb2lvlAJbrRvHJQ0b5EA==', '::1', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/84.0.4147.105 Safari/537.36', '2020-08-09 09:40:10', '1');
INSERT INTO `db_token` VALUES ('2092', '1', 'admin', '9zmG7wuq54l0y+7JRWlFPKeWzpTcqiwN1QhSSKg4QqkAb4C8BmxxySB+u3h/rHQ/geu3YXOrdBlXbVXRkPfaM4APtCb4UjCH6/91zqrPPNjROQmKR2FydLaMQTgf8F0hbaOnMjl5wnM8pMdUjFJCnYStfMqyfidiVp7sZ/mJJx9hfT2xrjGjlKfTLkGrZnU6SzYCJHpyorWQi55vsEISAm1A5l+pLUP35YF7BvVYwus=', '::1', 'Mozilla/5.0 (Linux; Android 6.0; Nexus 5 Build/MRA58N) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/76.0.3809.132 Mobile Safari/537.36', '2020-08-09 11:08:50', '1');
INSERT INTO `db_token` VALUES ('2095', '1', 'admin', '9zmG7wuq54l0y+7JRWlFPKeWzpTcqiwN1QhSSKg4QqkAb4C8BmxxySB+u3h/rHQ/gK6X+GHLanVa8ydTmF9ZD/wtD9KuJyfvQFKP+aPryfXQZS81GpNOH5mFiFj/Rvi8xy8ZJxgXfRJ9KZX28svbIH096ewImNqpTezgdBOBjU8NC9GEmvW+xSRH5B3VfUi/Skcb2lvlAJbrRvHJQ0b5EA==', '::1', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/76.0.3809.132 Safari/537.36', '2020-08-09 16:53:34', '1');
INSERT INTO `db_token` VALUES ('2096', '1', 'admin', '9zmG7wuq54l0y+7JRWlFPKeWzpTcqiwN1QhSSKg4QqkAb4C8BmxxySB+u3h/rHQ/geu3YXOrdBlXbVXRkPfaM4APtCb4UjCH6/91zqrPPNjROQmKR2FydLaMQTgf8F0hbaOnMjl5wnM8pMdUjFJCnYStfMqyfidiVp7sZ/mJJx9hfT2xrjGjlKfTLkGrZnU6SzYCJHpyorWQi55vsEISAm1A5l+pLUP35YF7BvVYwus=', '::1', 'Mozilla/5.0 (Linux; Android 6.0; Nexus 5 Build/MRA58N) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/76.0.3809.132 Mobile Safari/537.36', '2020-08-09 17:01:07', '1');
INSERT INTO `db_token` VALUES ('2099', '1', 'admin', '9zmG7wuq54l0y+7JRWlFPKeWzpTcqiwN1QhSSKg4QqkAb4C8BmxxySB+u3h/rHQ/gK6X+GHLanVa8ydTmF9ZD/wtD9KuJyfvQFKP+aPryfXQZS81GpNOH5mFiFj/Rvi8xy8ZJxgXfRJ9KZX28svbIOMnSPOCNTLqdu+IkOP5O0XFdgrORZhDdq0sQywuwVgzbS/T3uje2pDXDWOO/Hjipg==', '::1', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/83.0.4103.97 Safari/537.36', '2020-08-09 20:18:35', '1');
INSERT INTO `db_token` VALUES ('2100', '1', 'admin', '9zmG7wuq54l0y+7JRWlFPKeWzpTcqiwN1QhSSKg4QqkAb4C8BmxxySB+u3h/rHQ/6yDQdRSmuyCLNv5Gq+f7dhJr7Fb/yhm5JElj23rOoS8ssmSdFCESYFIa+Zx6I1pLLuMg5Yi5kV+MI11G0EGnB+EHSyIc7kbN2f+knYxe1kHw/e4CDYk8Uhe95N8hN7Ohsj9KvgTBJAb9WSU9wsW851cIy7ThW+X1kCAkfbyhoLvGcB9rgLX1VL5DwoJ/h7Ep', '::1', 'Mozilla/5.0 (iPhone; CPU iPhone OS 13_2_3 like Mac OS X) AppleWebKit/605.1.15 (KHTML, like Gecko) Version/13.0.3 Mobile/15E148 Safari/604.1', '2020-08-09 20:21:05', '1');
INSERT INTO `db_token` VALUES ('2101', '1', 'admin', '9zmG7wuq54l0y+7JRWlFPKeWzpTcqiwN1QhSSKg4QqkAb4C8BmxxySB+u3h/rHQ/gK6X+GHLanVa8ydTmF9ZD/wtD9KuJyfvQFKP+aPryfXQZS81GpNOH5mFiFj/Rvi8xy8ZJxgXfRJ9KZX28svbIOMnSPOCNTLqdu+IkOP5O0XFdgrORZhDdq0sQywuwVgzbS/T3uje2pDXDWOO/Hjipg==', '::1', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/83.0.4103.97 Safari/537.36', '2020-08-09 20:21:32', '1');
INSERT INTO `db_token` VALUES ('2104', '1', 'admin', '9zmG7wuq54l0y+7JRWlFPKeWzpTcqiwN1QhSSKg4QqkAb4C8BmxxySB+u3h/rHQ/gK6X+GHLanVa8ydTmF9ZD/wtD9KuJyfvQFKP+aPryfXQZS81GpNOH5mFiFj/Rvi8xy8ZJxgXfRJ9KZX28svbIH096ewImNqpTezgdBOBjU8NC9GEmvW+xSRH5B3VfUi/Skcb2lvlAJbrRvHJQ0b5EA==', '::1', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/76.0.3809.132 Safari/537.36', '2020-08-10 09:13:58', '1');
INSERT INTO `db_token` VALUES ('2105', '1', 'admin', '9zmG7wuq54l0y+7JRWlFPKeWzpTcqiwN1QhSSKg4QqkAb4C8BmxxySB+u3h/rHQ/geu3YXOrdBlXbVXRkPfaM4APtCb4UjCH6/91zqrPPNjROQmKR2FydLaMQTgf8F0hbaOnMjl5wnM8pMdUjFJCnYStfMqyfidiVp7sZ/mJJx9hfT2xrjGjlKfTLkGrZnU6SzYCJHpyorWQi55vsEISAm1A5l+pLUP35YF7BvVYwus=', '::1', 'Mozilla/5.0 (Linux; Android 6.0; Nexus 5 Build/MRA58N) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/76.0.3809.132 Mobile Safari/537.36', '2020-08-10 09:14:35', '1');
INSERT INTO `db_token` VALUES ('2108', '1', 'admin', '9zmG7wuq54l0y+7JRWlFPKeWzpTcqiwN1QhSSKg4QqkAb4C8BmxxySB+u3h/rHQ/geu3YXOrdBlXbVXRkPfaM4APtCb4UjCH6/91zqrPPNjROQmKR2FydLaMQTgf8F0hbaOnMjl5wnM8pMdUjFJCnYStfMqyfidiVp7sZ/mJJx9hfT2xrjGjlKfTLkGrZnU6SzYCJHpyorWQi55vsEISAm1A5l+pLUP35YF7BvVYwus=', '::1', 'Mozilla/5.0 (Linux; Android 6.0; Nexus 5 Build/MRA58N) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/76.0.3809.132 Mobile Safari/537.36', '2020-08-10 10:40:14', '1');
INSERT INTO `db_token` VALUES ('2109', '1', 'admin', '9zmG7wuq54l0y+7JRWlFPKeWzpTcqiwN1QhSSKg4QqkAb4C8BmxxySB+u3h/rHQ/gK6X+GHLanVa8ydTmF9ZD/wtD9KuJyfvQFKP+aPryfXQZS81GpNOH5mFiFj/Rvi8xy8ZJxgXfRJ9KZX28svbIEIXXVjxqzWduWZVx0ijT3or9OQKtEh8vR8MIPSrafh6Skcb2lvlAJbrRvHJQ0b5EA==', '::1', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/84.0.4147.105 Safari/537.36', '2020-08-10 10:56:38', '1');
INSERT INTO `db_token` VALUES ('2110', '1', 'admin', '9zmG7wuq54l0y+7JRWlFPKeWzpTcqiwN1QhSSKg4QqkAb4C8BmxxySB+u3h/rHQ/geu3YXOrdBlXbVXRkPfaM4APtCb4UjCH6/91zqrPPNjROQmKR2FydLaMQTgf8F0hbaOnMjl5wnM8pMdUjFJCnYStfMqyfidiVp7sZ/mJJx9hfT2xrjGjlKfTLkGrZnU6SzYCJHpyorWQi55vsEISAm1A5l+pLUP35YF7BvVYwus=', '::1', 'Mozilla/5.0 (Linux; Android 6.0; Nexus 5 Build/MRA58N) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/76.0.3809.132 Mobile Safari/537.36', '2020-08-10 11:26:33', '1');
INSERT INTO `db_token` VALUES ('2112', '1', 'admin', '9zmG7wuq54l0y+7JRWlFPKeWzpTcqiwN1QhSSKg4QqkAb4C8BmxxySB+u3h/rHQ/gK6X+GHLanVa8ydTmF9ZD/wtD9KuJyfvQFKP+aPryfXQZS81GpNOH5mFiFj/Rvi8xy8ZJxgXfRJ9KZX28svbIEIXXVjxqzWduWZVx0ijT3or9OQKtEh8vR8MIPSrafh6Skcb2lvlAJbrRvHJQ0b5EA==', '::1', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/84.0.4147.105 Safari/537.36', '2020-08-11 17:46:09', '1');
INSERT INTO `db_token` VALUES ('2122', '1', 'admin', '9zmG7wuq54l0y+7JRWlFPKeWzpTcqiwN1QhSSKg4QqkAb4C8BmxxySB+u3h/rHQ/gK6X+GHLanVa8ydTmF9ZD/wtD9KuJyfvQFKP+aPryfXQZS81GpNOH5mFiFj/Rvi8xy8ZJxgXfRJ9KZX28svbIEIXXVjxqzWduWZVx0ijT3or9OQKtEh8vR8MIPSrafh6Skcb2lvlAJbrRvHJQ0b5EA==', '::1', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/84.0.4147.105 Safari/537.36', '2020-08-14 17:12:22', '1');
INSERT INTO `db_token` VALUES ('2124', '1', 'admin', '9zmG7wuq54l0y+7JRWlFPKKgJPs96uF7owcL8wYBuKnpL63veDNEk0P1gXEb05MBet7QvazKl8C2zT4koRKoTotOfh9CjwQyXN/sW/2kr7E9mR22cmN0pbrm9AdRWMhPVgTfWqgJyZ4ImcJebPiZ5SLYYbIxJ97Ke2x2yvfbEzoKaHf/MBKZsV/fX83eCdOzqYtdjp3LD2AkD9UWBfNqJA==', '127.0.0.1', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/76.0.3809.132 Safari/537.36', '2020-08-15 14:44:17', '1');
INSERT INTO `db_token` VALUES ('2125', '1', 'admin', '9zmG7wuq54l0y+7JRWlFPKeWzpTcqiwN1QhSSKg4QqkAb4C8BmxxySB+u3h/rHQ/gK6X+GHLanVa8ydTmF9ZD/wtD9KuJyfvQFKP+aPryfXQZS81GpNOH5mFiFj/Rvi8xy8ZJxgXfRJ9KZX28svbIH096ewImNqpTezgdBOBjU8NC9GEmvW+xSRH5B3VfUi/Skcb2lvlAJbrRvHJQ0b5EA==', '::1', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/76.0.3809.132 Safari/537.36', '2020-08-15 14:44:37', '1');
INSERT INTO `db_token` VALUES ('2129', '1', 'admin', '9zmG7wuq54l0y+7JRWlFPKeWzpTcqiwN1QhSSKg4QqkAb4C8BmxxySB+u3h/rHQ/gK6X+GHLanVa8ydTmF9ZD/wtD9KuJyfvQFKP+aPryfXQZS81GpNOH5mFiFj/Rvi8xy8ZJxgXfRJ9KZX28svbIH096ewImNqpTezgdBOBjU8NC9GEmvW+xSRH5B3VfUi/Skcb2lvlAJbrRvHJQ0b5EA==', '::1', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/76.0.3809.132 Safari/537.36', '2020-08-16 09:05:50', '1');
INSERT INTO `db_token` VALUES ('2131', '1', 'admin', '9zmG7wuq54l0y+7JRWlFPKeWzpTcqiwN1QhSSKg4QqkAb4C8BmxxySB+u3h/rHQ/gK6X+GHLanVa8ydTmF9ZD/wtD9KuJyfvQFKP+aPryfXQZS81GpNOH5mFiFj/Rvi8xy8ZJxgXfRJ9KZX28svbIEIXXVjxqzWduWZVx0ijT3rWA3TXvVlQLhPSCxVoUFSZSkcb2lvlAJbrRvHJQ0b5EA==', '::1', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/84.0.4147.125 Safari/537.36', '2020-08-16 09:46:56', '1');
INSERT INTO `db_token` VALUES ('2135', '1', 'admin', '9zmG7wuq54l0y+7JRWlFPKeWzpTcqiwN1QhSSKg4QqkAb4C8BmxxySB+u3h/rHQ/gK6X+GHLanVa8ydTmF9ZD/wtD9KuJyfvQFKP+aPryfXQZS81GpNOH5mFiFj/Rvi8xy8ZJxgXfRJ9KZX28svbIEIXXVjxqzWduWZVx0ijT3rWA3TXvVlQLhPSCxVoUFSZSkcb2lvlAJbrRvHJQ0b5EA==', '::1', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/84.0.4147.125 Safari/537.36', '2020-08-16 13:16:16', '1');
INSERT INTO `db_token` VALUES ('2145', '1', 'admin', '9zmG7wuq54l0y+7JRWlFPKeWzpTcqiwN1QhSSKg4QqkAb4C8BmxxySB+u3h/rHQ/gK6X+GHLanVa8ydTmF9ZD/wtD9KuJyfvQFKP+aPryfXQZS81GpNOH5mFiFj/Rvi8xy8ZJxgXfRJ9KZX28svbIH096ewImNqpTezgdBOBjU8NC9GEmvW+xSRH5B3VfUi/Skcb2lvlAJbrRvHJQ0b5EA==', '::1', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/76.0.3809.132 Safari/537.36', '2020-08-16 16:09:11', '1');
INSERT INTO `db_token` VALUES ('2155', '1', 'admin', '9zmG7wuq54l0y+7JRWlFPKeWzpTcqiwN1QhSSKg4QqkAb4C8BmxxySB+u3h/rHQ/gK6X+GHLanVa8ydTmF9ZD/wtD9KuJyfvQFKP+aPryfXQZS81GpNOH5mFiFj/Rvi8xy8ZJxgXfRJ9KZX28svbIH096ewImNqpTezgdBOBjU8NC9GEmvW+xSRH5B3VfUi/Skcb2lvlAJbrRvHJQ0b5EA==', '::1', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/76.0.3809.132 Safari/537.36', '2020-08-17 09:13:16', '1');
INSERT INTO `db_token` VALUES ('2164', '1', 'admin', '9zmG7wuq54l0y+7JRWlFPKeWzpTcqiwN1QhSSKg4QqkAb4C8BmxxySB+u3h/rHQ/geu3YXOrdBlXbVXRkPfaM4APtCb4UjCH6/91zqrPPNjROQmKR2FydLaMQTgf8F0hbaOnMjl5wnM8pMdUjFJCnYStfMqyfidiVp7sZ/mJJx9hfT2xrjGjlKfTLkGrZnU6SzYCJHpyorWQi55vsEISAm1A5l+pLUP35YF7BvVYwus=', '::1', 'Mozilla/5.0 (Linux; Android 6.0; Nexus 5 Build/MRA58N) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/76.0.3809.132 Mobile Safari/537.36', '2020-08-17 11:03:42', '1');
INSERT INTO `db_token` VALUES ('2165', '1', 'admin', '9zmG7wuq54l0y+7JRWlFPKeWzpTcqiwN1QhSSKg4QqkAb4C8BmxxySB+u3h/rHQ/gK6X+GHLanVa8ydTmF9ZD/wtD9KuJyfvQFKP+aPryfXQZS81GpNOH5mFiFj/Rvi8xy8ZJxgXfRJ9KZX28svbIH096ewImNqpTezgdBOBjU8NC9GEmvW+xSRH5B3VfUi/Skcb2lvlAJbrRvHJQ0b5EA==', '::1', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/76.0.3809.132 Safari/537.36', '2020-08-17 11:10:39', '1');
INSERT INTO `db_token` VALUES ('2168', '1', 'admin', '9zmG7wuq54l0y+7JRWlFPKeWzpTcqiwN1QhSSKg4QqkAb4C8BmxxySB+u3h/rHQ/geu3YXOrdBlXbVXRkPfaM4APtCb4UjCH6/91zqrPPNjROQmKR2FydLaMQTgf8F0hbaOnMjl5wnM8pMdUjFJCnYStfMqyfidiVp7sZ/mJJx9hfT2xrjGjlKfTLkGrZnU6SzYCJHpyorWQi55vsEISAm1A5l+pLUP35YF7BvVYwus=', '::1', 'Mozilla/5.0 (Linux; Android 6.0; Nexus 5 Build/MRA58N) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/76.0.3809.132 Mobile Safari/537.36', '2020-08-17 14:09:50', '1');
INSERT INTO `db_token` VALUES ('2171', '1', 'admin', '9zmG7wuq54l0y+7JRWlFPKeWzpTcqiwN1QhSSKg4QqkAb4C8BmxxySB+u3h/rHQ/geu3YXOrdBlXbVXRkPfaM4APtCb4UjCH6/91zqrPPNjROQmKR2FydLaMQTgf8F0hbaOnMjl5wnM8pMdUjFJCnYStfMqyfidiVp7sZ/mJJx9hfT2xrjGjlKfTLkGrZnU6SzYCJHpyorWQi55vsEISAm1A5l+pLUP35YF7BvVYwus=', '::1', 'Mozilla/5.0 (Linux; Android 6.0; Nexus 5 Build/MRA58N) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/76.0.3809.132 Mobile Safari/537.36', '2020-08-17 16:40:55', '1');
INSERT INTO `db_token` VALUES ('2177', '1', 'admin', '9zmG7wuq54l0y+7JRWlFPKeWzpTcqiwN1QhSSKg4QqkAb4C8BmxxySB+u3h/rHQ/gK6X+GHLanVa8ydTmF9ZD/wtD9KuJyfvQFKP+aPryfXQZS81GpNOH5mFiFj/Rvi8xy8ZJxgXfRJ9KZX28svbIH096ewImNqpTezgdBOBjU8NC9GEmvW+xSRH5B3VfUi/Skcb2lvlAJbrRvHJQ0b5EA==', '::1', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/76.0.3809.132 Safari/537.36', '2020-08-20 09:47:49', '1');
INSERT INTO `db_token` VALUES ('2178', '1', 'admin', '9zmG7wuq54l0y+7JRWlFPKeWzpTcqiwN1QhSSKg4QqkAb4C8BmxxySB+u3h/rHQ/gK6X+GHLanVa8ydTmF9ZD/wtD9KuJyfvQFKP+aPryfXQZS81GpNOH5mFiFj/Rvi8xy8ZJxgXfRJ9KZX28svbIEIXXVjxqzWduWZVx0ijT3rWA3TXvVlQLhPSCxVoUFSZSkcb2lvlAJbrRvHJQ0b5EA==', '::1', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/84.0.4147.125 Safari/537.36', '2020-08-20 09:50:52', '1');
INSERT INTO `db_token` VALUES ('2188', '1', 'admin', '9zmG7wuq54l0y+7JRWlFPKeWzpTcqiwN1QhSSKg4QqkAb4C8BmxxySB+u3h/rHQ/geu3YXOrdBlXbVXRkPfaM4APtCb4UjCH6/91zqrPPNjROQmKR2FydLaMQTgf8F0hbaOnMjl5wnM8pMdUjFJCnYStfMqyfidiVp7sZ/mJJx9hfT2xrjGjlKfTLkGrZnU6SzYCJHpyorWQi55vsEISAm1A5l+pLUP35YF7BvVYwus=', '::1', 'Mozilla/5.0 (Linux; Android 6.0; Nexus 5 Build/MRA58N) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/76.0.3809.132 Mobile Safari/537.36', '2020-08-20 11:44:38', '1');
INSERT INTO `db_token` VALUES ('2189', '1', 'admin', '9zmG7wuq54l0y+7JRWlFPKeWzpTcqiwN1QhSSKg4QqkAb4C8BmxxySB+u3h/rHQ/gK6X+GHLanVa8ydTmF9ZD/wtD9KuJyfvQFKP+aPryfXQZS81GpNOH5mFiFj/Rvi8xy8ZJxgXfRJ9KZX28svbIH096ewImNqpTezgdBOBjU8NC9GEmvW+xSRH5B3VfUi/Skcb2lvlAJbrRvHJQ0b5EA==', '::1', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/76.0.3809.132 Safari/537.36', '2020-08-20 11:52:11', '1');
INSERT INTO `db_token` VALUES ('2199', '1', 'admin', '9zmG7wuq54l0y+7JRWlFPKeWzpTcqiwN1QhSSKg4QqkAb4C8BmxxySB+u3h/rHQ/gK6X+GHLanVa8ydTmF9ZD/wtD9KuJyfvQFKP+aPryfXQZS81GpNOH5mFiFj/Rvi8xy8ZJxgXfRJ9KZX28svbIEIXXVjxqzWduWZVx0ijT3rWA3TXvVlQLhPSCxVoUFSZSkcb2lvlAJbrRvHJQ0b5EA==', '::1', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/84.0.4147.125 Safari/537.36', '2020-08-20 14:16:26', '1');
INSERT INTO `db_token` VALUES ('2202', '1', 'admin', '9zmG7wuq54l0y+7JRWlFPKeWzpTcqiwN1QhSSKg4QqkAb4C8BmxxySB+u3h/rHQ/gK6X+GHLanVa8ydTmF9ZD/wtD9KuJyfvQFKP+aPryfXQZS81GpNOH5mFiFj/Rvi8xy8ZJxgXfRJ9KZX28svbIEIXXVjxqzWduWZVx0ijT3rWA3TXvVlQLhPSCxVoUFSZSkcb2lvlAJbrRvHJQ0b5EA==', '::1', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/84.0.4147.125 Safari/537.36', '2020-08-20 14:35:19', '1');
INSERT INTO `db_token` VALUES ('2205', '1', 'admin', '9zmG7wuq54l0y+7JRWlFPKeWzpTcqiwN1QhSSKg4QqkAb4C8BmxxySB+u3h/rHQ/gK6X+GHLanVa8ydTmF9ZD/wtD9KuJyfvQFKP+aPryfXQZS81GpNOH5mFiFj/Rvi8xy8ZJxgXfRJ9KZX28svbIH096ewImNqpTezgdBOBjU8NC9GEmvW+xSRH5B3VfUi/Skcb2lvlAJbrRvHJQ0b5EA==', '::1', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/76.0.3809.132 Safari/537.36', '2020-08-20 14:50:55', '1');
INSERT INTO `db_token` VALUES ('2206', '1', 'admin', '9zmG7wuq54l0y+7JRWlFPKeWzpTcqiwN1QhSSKg4QqkAb4C8BmxxySB+u3h/rHQ/gK6X+GHLanVa8ydTmF9ZD/wtD9KuJyfvQFKP+aPryfXQZS81GpNOH5mFiFj/Rvi8xy8ZJxgXfRJ9KZX28svbIH096ewImNqpTezgdBOBjU8NC9GEmvW+xSRH5B3VfUi/Skcb2lvlAJbrRvHJQ0b5EA==', '::1', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/76.0.3809.132 Safari/537.36', '2020-08-20 14:51:01', '1');
INSERT INTO `db_token` VALUES ('2207', '1', 'admin', '9zmG7wuq54l0y+7JRWlFPKeWzpTcqiwN1QhSSKg4QqkAb4C8BmxxySB+u3h/rHQ/geu3YXOrdBlXbVXRkPfaM4APtCb4UjCH6/91zqrPPNjROQmKR2FydLaMQTgf8F0hbaOnMjl5wnM8pMdUjFJCnYStfMqyfidiVp7sZ/mJJx9hfT2xrjGjlKfTLkGrZnU6SzYCJHpyorWQi55vsEISAm1A5l+pLUP35YF7BvVYwus=', '::1', 'Mozilla/5.0 (Linux; Android 6.0; Nexus 5 Build/MRA58N) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/76.0.3809.132 Mobile Safari/537.36', '2020-08-20 14:51:41', '1');
INSERT INTO `db_token` VALUES ('2208', '1', 'admin', '9zmG7wuq54l0y+7JRWlFPKeWzpTcqiwN1QhSSKg4QqkAb4C8BmxxySB+u3h/rHQ/geu3YXOrdBlXbVXRkPfaM4APtCb4UjCH6/91zqrPPNjROQmKR2FydLaMQTgf8F0hbaOnMjl5wnM8pMdUjFJCnYStfMqyfidiVp7sZ/mJJx9hfT2xrjGjlKfTLkGrZnU6SzYCJHpyorWQi55vsEISAm1A5l+pLUP35YF7BvVYwus=', '::1', 'Mozilla/5.0 (Linux; Android 6.0; Nexus 5 Build/MRA58N) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/76.0.3809.132 Mobile Safari/537.36', '2020-08-20 14:52:11', '1');
INSERT INTO `db_token` VALUES ('2209', '1', 'admin', '9zmG7wuq54l0y+7JRWlFPKeWzpTcqiwN1QhSSKg4QqkAb4C8BmxxySB+u3h/rHQ/geu3YXOrdBlXbVXRkPfaM4APtCb4UjCH6/91zqrPPNjROQmKR2FydLaMQTgf8F0hbaOnMjl5wnM8pMdUjFJCnYStfMqyfidiVp7sZ/mJJx9hfT2xrjGjlKfTLkGrZnU6SzYCJHpyorWQi55vsEISAm1A5l+pLUP35YF7BvVYwus=', '::1', 'Mozilla/5.0 (Linux; Android 6.0; Nexus 5 Build/MRA58N) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/76.0.3809.132 Mobile Safari/537.36', '2020-08-20 14:52:25', '1');
INSERT INTO `db_token` VALUES ('2210', '1', 'admin', '9zmG7wuq54l0y+7JRWlFPKeWzpTcqiwN1QhSSKg4QqkAb4C8BmxxySB+u3h/rHQ/gK6X+GHLanVa8ydTmF9ZD/wtD9KuJyfvQFKP+aPryfXQZS81GpNOH5mFiFj/Rvi8xy8ZJxgXfRJ9KZX28svbIEIXXVjxqzWduWZVx0ijT3rWA3TXvVlQLhPSCxVoUFSZSkcb2lvlAJbrRvHJQ0b5EA==', '::1', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/84.0.4147.125 Safari/537.36', '2020-08-20 14:52:47', '1');
INSERT INTO `db_token` VALUES ('2211', '1', 'admin', '9zmG7wuq54l0y+7JRWlFPKeWzpTcqiwN1QhSSKg4QqkAb4C8BmxxySB+u3h/rHQ/gK6X+GHLanVa8ydTmF9ZD/wtD9KuJyfvQFKP+aPryfXQZS81GpNOH5mFiFj/Rvi8xy8ZJxgXfRJ9KZX28svbIH096ewImNqpTezgdBOBjU8NC9GEmvW+xSRH5B3VfUi/Skcb2lvlAJbrRvHJQ0b5EA==', '::1', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/76.0.3809.132 Safari/537.36', '2020-08-20 14:53:05', '1');
INSERT INTO `db_token` VALUES ('2212', '1', 'admin', '9zmG7wuq54l0y+7JRWlFPKeWzpTcqiwN1QhSSKg4QqkAb4C8BmxxySB+u3h/rHQ/geu3YXOrdBlXbVXRkPfaM4APtCb4UjCH6/91zqrPPNjROQmKR2FydLaMQTgf8F0hbaOnMjl5wnM8pMdUjFJCnYStfMqyfidiVp7sZ/mJJx9hfT2xrjGjlKfTLkGrZnU6SzYCJHpyorWQi55vsEISAm1A5l+pLUP35YF7BvVYwus=', '::1', 'Mozilla/5.0 (Linux; Android 6.0; Nexus 5 Build/MRA58N) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/76.0.3809.132 Mobile Safari/537.36', '2020-08-20 15:01:48', '1');
INSERT INTO `db_token` VALUES ('2219', '1', 'admin', '9zmG7wuq54l0y+7JRWlFPKeWzpTcqiwN1QhSSKg4QqkAb4C8BmxxySB+u3h/rHQ/gK6X+GHLanVa8ydTmF9ZD/wtD9KuJyfvQFKP+aPryfXQZS81GpNOH5mFiFj/Rvi8xy8ZJxgXfRJ9KZX28svbIEIXXVjxqzWduWZVx0ijT3rWA3TXvVlQLhPSCxVoUFSZSkcb2lvlAJbrRvHJQ0b5EA==', '::1', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/84.0.4147.125 Safari/537.36', '2020-08-20 16:34:47', '1');
INSERT INTO `db_token` VALUES ('2227', '1', 'admin', '9zmG7wuq54l0y+7JRWlFPKeWzpTcqiwN1QhSSKg4QqkAb4C8BmxxySB+u3h/rHQ/geu3YXOrdBlXbVXRkPfaM4APtCb4UjCH6/91zqrPPNjROQmKR2FydLaMQTgf8F0hbaOnMjl5wnM8pMdUjFJCnYStfMqyfidiVp7sZ/mJJx9hfT2xrjGjlKfTLkGrZnU6SzYCJHpyorWQi55vsEISAm1A5l+pLUP35YF7BvVYwus=', '::1', 'Mozilla/5.0 (Linux; Android 6.0; Nexus 5 Build/MRA58N) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/76.0.3809.132 Mobile Safari/537.36', '2020-08-21 09:07:40', '1');
INSERT INTO `db_token` VALUES ('2229', '1', 'admin', '9zmG7wuq54l0y+7JRWlFPKeWzpTcqiwN1QhSSKg4QqkAb4C8BmxxySB+u3h/rHQ/gK6X+GHLanVa8ydTmF9ZD/wtD9KuJyfvQFKP+aPryfXQZS81GpNOH5mFiFj/Rvi8xy8ZJxgXfRJ9KZX28svbIEIXXVjxqzWduWZVx0ijT3rWA3TXvVlQLhPSCxVoUFSZSkcb2lvlAJbrRvHJQ0b5EA==', '::1', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/84.0.4147.125 Safari/537.36', '2020-08-21 09:32:37', '1');
INSERT INTO `db_token` VALUES ('2236', '1', 'admin', '9zmG7wuq54l0y+7JRWlFPKeWzpTcqiwN1QhSSKg4QqkAb4C8BmxxySB+u3h/rHQ/gK6X+GHLanVa8ydTmF9ZD/wtD9KuJyfvQFKP+aPryfXQZS81GpNOH5mFiFj/Rvi8xy8ZJxgXfRJ9KZX28svbIH096ewImNqpTezgdBOBjU8NC9GEmvW+xSRH5B3VfUi/Skcb2lvlAJbrRvHJQ0b5EA==', '::1', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/76.0.3809.132 Safari/537.36', '2020-08-21 14:07:59', '1');
INSERT INTO `db_token` VALUES ('2239', '1', 'admin', '9zmG7wuq54l0y+7JRWlFPKeWzpTcqiwN1QhSSKg4QqkAb4C8BmxxySB+u3h/rHQ/geu3YXOrdBlXbVXRkPfaM4APtCb4UjCH6/91zqrPPNjROQmKR2FydLaMQTgf8F0hbaOnMjl5wnM8pMdUjFJCnYStfMqyfidiVp7sZ/mJJx9hfT2xrjGjlKfTLkGrZnU6SzYCJHpyorWQi55vsEISAm1A5l+pLUP35YF7BvVYwus=', '::1', 'Mozilla/5.0 (Linux; Android 6.0; Nexus 5 Build/MRA58N) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/76.0.3809.132 Mobile Safari/537.36', '2020-08-21 14:32:59', '1');
INSERT INTO `db_token` VALUES ('2241', '1', 'admin', '9zmG7wuq54l0y+7JRWlFPKKgJPs96uF7owcL8wYBuKnpL63veDNEk0P1gXEb05MBSqqUCY410S9Q6Jbr7P1JrQIopB0u+x8wc+kG55lvpcrLDaSt6lSiku0SZHAtOGmL9inab4gj52mA7Bir0vow5xWTpDZii5dG+nP5JISIXevICwdsSyD3yqsjI4DmDgbiVjkSe/0e/hQD+orkCdxxgWSwGuiSeljRHUMikTk6IWbeUpFEKoIiwgq6/pvhqHmt', '127.0.0.1', 'Mozilla/5.0 (Linux; Android 6.0; Nexus 5 Build/MRA58N) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/76.0.3809.132 Mobile Safari/537.36', '2020-08-21 14:58:01', '1');
INSERT INTO `db_token` VALUES ('2242', '1', 'admin', '9zmG7wuq54l0y+7JRWlFPKeWzpTcqiwN1QhSSKg4QqkAb4C8BmxxySB+u3h/rHQ/geu3YXOrdBlXbVXRkPfaM4APtCb4UjCH6/91zqrPPNjROQmKR2FydLaMQTgf8F0hbaOnMjl5wnM8pMdUjFJCnYStfMqyfidiVp7sZ/mJJx9hfT2xrjGjlKfTLkGrZnU6SzYCJHpyorWQi55vsEISAm1A5l+pLUP35YF7BvVYwus=', '::1', 'Mozilla/5.0 (Linux; Android 6.0; Nexus 5 Build/MRA58N) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/76.0.3809.132 Mobile Safari/537.36', '2020-08-21 14:59:18', '1');
INSERT INTO `db_token` VALUES ('2245', '1', 'admin', '9zmG7wuq54l0y+7JRWlFPKeWzpTcqiwN1QhSSKg4QqkAb4C8BmxxySB+u3h/rHQ/6yDQdRSmuyCLNv5Gq+f7dhJr7Fb/yhm5JElj23rOoS8ssmSdFCESYFIa+Zx6I1pLLuMg5Yi5kV+MI11G0EGnB+EHSyIc7kbN2f+knYxe1kHw/e4CDYk8Uhe95N8hN7Ohsj9KvgTBJAb9WSU9wsW851cIy7ThW+X1kCAkfbyhoLvGcB9rgLX1VL5DwoJ/h7Ep', '::1', 'Mozilla/5.0 (iPhone; CPU iPhone OS 13_2_3 like Mac OS X) AppleWebKit/605.1.15 (KHTML, like Gecko) Version/13.0.3 Mobile/15E148 Safari/604.1', '2020-08-21 15:19:13', '1');
INSERT INTO `db_token` VALUES ('2246', '1', 'admin', '9zmG7wuq54l0y+7JRWlFPKeWzpTcqiwN1QhSSKg4QqkAb4C8BmxxySB+u3h/rHQ/gK6X+GHLanVa8ydTmF9ZD/wtD9KuJyfvQFKP+aPryfXQZS81GpNOH5mFiFj/Rvi8xy8ZJxgXfRJ9KZX28svbIEIXXVjxqzWduWZVx0ijT3rWA3TXvVlQLhPSCxVoUFSZSkcb2lvlAJbrRvHJQ0b5EA==', '::1', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/84.0.4147.125 Safari/537.36', '2020-08-21 15:19:32', '1');
INSERT INTO `db_token` VALUES ('2252', '1', 'admin', '9zmG7wuq54l0y+7JRWlFPKeWzpTcqiwN1QhSSKg4QqkAb4C8BmxxySB+u3h/rHQ/gK6X+GHLanVa8ydTmF9ZD/wtD9KuJyfvQFKP+aPryfXQZS81GpNOH5mFiFj/Rvi8xy8ZJxgXfRJ9KZX28svbIEIXXVjxqzWduWZVx0ijT3rWA3TXvVlQLhPSCxVoUFSZSkcb2lvlAJbrRvHJQ0b5EA==', '::1', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/84.0.4147.125 Safari/537.36', '2020-08-21 18:54:51', '1');
INSERT INTO `db_token` VALUES ('2253', '1', 'admin', '9zmG7wuq54l0y+7JRWlFPKeWzpTcqiwN1QhSSKg4QqkAb4C8BmxxySB+u3h/rHQ/gK6X+GHLanVa8ydTmF9ZD/wtD9KuJyfvQFKP+aPryfXQZS81GpNOH5mFiFj/Rvi8xy8ZJxgXfRJ9KZX28svbIH096ewImNqpTezgdBOBjU8NC9GEmvW+xSRH5B3VfUi/Skcb2lvlAJbrRvHJQ0b5EA==', '::1', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/76.0.3809.132 Safari/537.36', '2020-08-22 09:34:27', '1');
INSERT INTO `db_token` VALUES ('2254', '1', 'admin', '9zmG7wuq54l0y+7JRWlFPKeWzpTcqiwN1QhSSKg4QqkAb4C8BmxxySB+u3h/rHQ/gK6X+GHLanVa8ydTmF9ZD/wtD9KuJyfvQFKP+aPryfXQZS81GpNOH5mFiFj/Rvi8xy8ZJxgXfRJ9KZX28svbIEIXXVjxqzWduWZVx0ijT3rWA3TXvVlQLhPSCxVoUFSZSkcb2lvlAJbrRvHJQ0b5EA==', '::1', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/84.0.4147.125 Safari/537.36', '2020-08-22 09:57:57', '1');
INSERT INTO `db_token` VALUES ('2258', '1', 'admin', '9zmG7wuq54l0y+7JRWlFPKeWzpTcqiwN1QhSSKg4QqkAb4C8BmxxySB+u3h/rHQ/gK6X+GHLanVa8ydTmF9ZD/wtD9KuJyfvQFKP+aPryfXQZS81GpNOH5mFiFj/Rvi8xy8ZJxgXfRJ9KZX28svbIEIXXVjxqzWduWZVx0ijT3rWA3TXvVlQLhPSCxVoUFSZSkcb2lvlAJbrRvHJQ0b5EA==', '::1', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/84.0.4147.125 Safari/537.36', '2020-08-22 11:03:39', '1');
INSERT INTO `db_token` VALUES ('2259', '1', 'admin', '9zmG7wuq54l0y+7JRWlFPKeWzpTcqiwN1QhSSKg4QqkAb4C8BmxxySB+u3h/rHQ/gK6X+GHLanVa8ydTmF9ZD/wtD9KuJyfvQFKP+aPryfXQZS81GpNOH5mFiFj/Rvi8xy8ZJxgXfRJ9KZX28svbIEIXXVjxqzWduWZVx0ijT3rWA3TXvVlQLhPSCxVoUFSZSkcb2lvlAJbrRvHJQ0b5EA==', '::1', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/84.0.4147.125 Safari/537.36', '2020-08-22 11:03:45', '1');
INSERT INTO `db_token` VALUES ('2260', '1', 'admin', '9zmG7wuq54l0y+7JRWlFPKeWzpTcqiwN1QhSSKg4QqkAb4C8BmxxySB+u3h/rHQ/gK6X+GHLanVa8ydTmF9ZD/wtD9KuJyfvQFKP+aPryfXQZS81GpNOH5mFiFj/Rvi8xy8ZJxgXfRJ9KZX28svbIEIXXVjxqzWduWZVx0ijT3rWA3TXvVlQLhPSCxVoUFSZSkcb2lvlAJbrRvHJQ0b5EA==', '::1', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/84.0.4147.125 Safari/537.36', '2020-08-22 11:03:59', '1');
INSERT INTO `db_token` VALUES ('2261', '1', 'admin', '9zmG7wuq54l0y+7JRWlFPKeWzpTcqiwN1QhSSKg4QqkAb4C8BmxxySB+u3h/rHQ/gK6X+GHLanVa8ydTmF9ZD/wtD9KuJyfvQFKP+aPryfXQZS81GpNOH5mFiFj/Rvi8xy8ZJxgXfRJ9KZX28svbIEIXXVjxqzWduWZVx0ijT3rWA3TXvVlQLhPSCxVoUFSZSkcb2lvlAJbrRvHJQ0b5EA==', '::1', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/84.0.4147.125 Safari/537.36', '2020-08-22 11:05:07', '1');
INSERT INTO `db_token` VALUES ('2262', '1', 'admin', '9zmG7wuq54l0y+7JRWlFPKeWzpTcqiwN1QhSSKg4QqkAb4C8BmxxySB+u3h/rHQ/gK6X+GHLanVa8ydTmF9ZD/wtD9KuJyfvQFKP+aPryfXQZS81GpNOH5mFiFj/Rvi8xy8ZJxgXfRJ9KZX28svbIEIXXVjxqzWduWZVx0ijT3qdigKApBz7e214zg3CRilBSkcb2lvlAJbrRvHJQ0b5EA==', '::1', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/84.0.4147.135 Safari/537.36', '2020-08-22 11:21:48', '1');
INSERT INTO `db_token` VALUES ('2263', '1', 'admin', '9zmG7wuq54l0y+7JRWlFPKeWzpTcqiwN1QhSSKg4QqkAb4C8BmxxySB+u3h/rHQ/geu3YXOrdBlXbVXRkPfaM4APtCb4UjCH6/91zqrPPNjROQmKR2FydLaMQTgf8F0hbaOnMjl5wnM8pMdUjFJCnYStfMqyfidiVp7sZ/mJJx9hfT2xrjGjlKfTLkGrZnU6SzYCJHpyorWQi55vsEISAm1A5l+pLUP35YF7BvVYwus=', '::1', 'Mozilla/5.0 (Linux; Android 6.0; Nexus 5 Build/MRA58N) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/76.0.3809.132 Mobile Safari/537.36', '2020-08-22 14:09:38', '1');
INSERT INTO `db_token` VALUES ('2266', '1', 'admin', '9zmG7wuq54l0y+7JRWlFPKeWzpTcqiwN1QhSSKg4QqkAb4C8BmxxySB+u3h/rHQ/gK6X+GHLanVa8ydTmF9ZD/wtD9KuJyfvQFKP+aPryfXQZS81GpNOH5mFiFj/Rvi8xy8ZJxgXfRJ9KZX28svbIEIXXVjxqzWduWZVx0ijT3qdigKApBz7e214zg3CRilBSkcb2lvlAJbrRvHJQ0b5EA==', '::1', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/84.0.4147.135 Safari/537.36', '2020-08-22 15:04:05', '1');
INSERT INTO `db_token` VALUES ('2267', '1', 'admin', '9zmG7wuq54l0y+7JRWlFPKeWzpTcqiwN1QhSSKg4QqkAb4C8BmxxySB+u3h/rHQ/gK6X+GHLanVa8ydTmF9ZD/wtD9KuJyfvQFKP+aPryfXQZS81GpNOH5mFiFj/Rvi8xy8ZJxgXfRJ9KZX28svbIEIXXVjxqzWduWZVx0ijT3qdigKApBz7e214zg3CRilBSkcb2lvlAJbrRvHJQ0b5EA==', '::1', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/84.0.4147.135 Safari/537.36', '2020-08-22 15:04:49', '1');
INSERT INTO `db_token` VALUES ('2268', '1', 'admin', '9zmG7wuq54l0y+7JRWlFPKeWzpTcqiwN1QhSSKg4QqkAb4C8BmxxySB+u3h/rHQ/gK6X+GHLanVa8ydTmF9ZD/wtD9KuJyfvQFKP+aPryfXQZS81GpNOH5mFiFj/Rvi8xy8ZJxgXfRJ9KZX28svbIH096ewImNqpTezgdBOBjU8NC9GEmvW+xSRH5B3VfUi/Skcb2lvlAJbrRvHJQ0b5EA==', '::1', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/76.0.3809.132 Safari/537.36', '2020-08-23 09:06:40', '1');
INSERT INTO `db_token` VALUES ('2270', '1', 'admin', '9zmG7wuq54l0y+7JRWlFPKeWzpTcqiwN1QhSSKg4QqkAb4C8BmxxySB+u3h/rHQ/gK6X+GHLanVa8ydTmF9ZD/wtD9KuJyfvQFKP+aPryfXQZS81GpNOH5mFiFj/Rvi8xy8ZJxgXfRJ9KZX28svbIEIXXVjxqzWduWZVx0ijT3qdigKApBz7e214zg3CRilBSkcb2lvlAJbrRvHJQ0b5EA==', '::1', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/84.0.4147.135 Safari/537.36', '2020-08-23 09:36:31', '1');
INSERT INTO `db_token` VALUES ('2272', '1', 'admin', '9zmG7wuq54l0y+7JRWlFPKeWzpTcqiwN1QhSSKg4QqkAb4C8BmxxySB+u3h/rHQ/geu3YXOrdBlXbVXRkPfaM4APtCb4UjCH6/91zqrPPNjROQmKR2FydLaMQTgf8F0hbaOnMjl5wnM8pMdUjFJCnYStfMqyfidiVp7sZ/mJJx9hfT2xrjGjlKfTLkGrZnU6SzYCJHpyorWQi55vsEISAm1A5l+pLUP35YF7BvVYwus=', '::1', 'Mozilla/5.0 (Linux; Android 6.0; Nexus 5 Build/MRA58N) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/76.0.3809.132 Mobile Safari/537.36', '2020-08-23 10:35:12', '1');
INSERT INTO `db_token` VALUES ('2275', '1', 'admin', '9zmG7wuq54l0y+7JRWlFPKeWzpTcqiwN1QhSSKg4QqkAb4C8BmxxySB+u3h/rHQ/geu3YXOrdBlXbVXRkPfaM4APtCb4UjCH6/91zqrPPNjROQmKR2FydLaMQTgf8F0hbaOnMjl5wnM8pMdUjFJCnYStfMqyfidiVp7sZ/mJJx9hfT2xrjGjlKfTLkGrZnU6SzYCJHpyorWQi55vsEISAm1A5l+pLUP35YF7BvVYwus=', '::1', 'Mozilla/5.0 (Linux; Android 6.0; Nexus 5 Build/MRA58N) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/76.0.3809.132 Mobile Safari/537.36', '2020-08-23 11:19:40', '1');
INSERT INTO `db_token` VALUES ('2287', '1', 'admin', '9zmG7wuq54l0y+7JRWlFPKeWzpTcqiwN1QhSSKg4QqkAb4C8BmxxySB+u3h/rHQ/gK6X+GHLanVa8ydTmF9ZD/wtD9KuJyfvQFKP+aPryfXQZS81GpNOH5mFiFj/Rvi8xy8ZJxgXfRJ9KZX28svbIH096ewImNqpTezgdBOBjU8NC9GEmvW+xSRH5B3VfUi/Skcb2lvlAJbrRvHJQ0b5EA==', '::1', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/76.0.3809.132 Safari/537.36', '2020-08-27 09:26:04', '1');
INSERT INTO `db_token` VALUES ('2288', '1', 'admin', '9zmG7wuq54l0y+7JRWlFPKeWzpTcqiwN1QhSSKg4QqkAb4C8BmxxySB+u3h/rHQ/gK6X+GHLanVa8ydTmF9ZD/wtD9KuJyfvQFKP+aPryfXQZS81GpNOH5mFiFj/Rvi8xy8ZJxgXfRJ9KZX28svbIH096ewImNqpTezgdBOBjU8NC9GEmvW+xSRH5B3VfUi/Skcb2lvlAJbrRvHJQ0b5EA==', '::1', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/76.0.3809.132 Safari/537.36', '2020-08-27 10:03:28', '1');
INSERT INTO `db_token` VALUES ('2290', '1', 'admin', '9zmG7wuq54l0y+7JRWlFPKeWzpTcqiwN1QhSSKg4QqkAb4C8BmxxySB+u3h/rHQ/gK6X+GHLanVa8ydTmF9ZD/wtD9KuJyfvQFKP+aPryfXQZS81GpNOH5mFiFj/Rvi8xy8ZJxgXfRJ9KZX28svbIH096ewImNqpTezgdBOBjU8NC9GEmvW+xSRH5B3VfUi/Skcb2lvlAJbrRvHJQ0b5EA==', '::1', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/76.0.3809.132 Safari/537.36', '2020-08-27 10:24:49', '1');
INSERT INTO `db_token` VALUES ('2292', '1', 'admin', '9zmG7wuq54l0y+7JRWlFPKeWzpTcqiwN1QhSSKg4QqkAb4C8BmxxySB+u3h/rHQ/gK6X+GHLanVa8ydTmF9ZD/wtD9KuJyfvQFKP+aPryfXQZS81GpNOH5mFiFj/Rvi8xy8ZJxgXfRJ9KZX28svbIH096ewImNqpTezgdBOBjU8NC9GEmvW+xSRH5B3VfUi/Skcb2lvlAJbrRvHJQ0b5EA==', '::1', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/76.0.3809.132 Safari/537.36', '2020-08-27 10:29:47', '1');
INSERT INTO `db_token` VALUES ('2293', '1', 'admin', '9zmG7wuq54l0y+7JRWlFPKeWzpTcqiwN1QhSSKg4QqkAb4C8BmxxySB+u3h/rHQ/gK6X+GHLanVa8ydTmF9ZD/wtD9KuJyfvQFKP+aPryfXQZS81GpNOH5mFiFj/Rvi8xy8ZJxgXfRJ9KZX28svbIEIXXVjxqzWduWZVx0ijT3qdigKApBz7e214zg3CRilBSkcb2lvlAJbrRvHJQ0b5EA==', '::1', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/84.0.4147.135 Safari/537.36', '2020-08-29 10:24:58', '1');
INSERT INTO `db_token` VALUES ('2294', '1', 'admin', '9zmG7wuq54l0y+7JRWlFPKeWzpTcqiwN1QhSSKg4QqkAb4C8BmxxySB+u3h/rHQ/gK6X+GHLanVa8ydTmF9ZD/wtD9KuJyfvQFKP+aPryfXQZS81GpNOH5mFiFj/Rvi8xy8ZJxgXfRJ9KZX28svbIEIXXVjxqzWduWZVx0ijT3qdigKApBz7e214zg3CRilBSkcb2lvlAJbrRvHJQ0b5EA==', '::1', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/84.0.4147.135 Safari/537.36', '2020-08-29 10:50:22', '1');
INSERT INTO `db_token` VALUES ('2295', '1', 'admin', '9zmG7wuq54l0y+7JRWlFPKeWzpTcqiwN1QhSSKg4QqkAb4C8BmxxySB+u3h/rHQ/gK6X+GHLanVa8ydTmF9ZD/wtD9KuJyfvQFKP+aPryfXQZS81GpNOH5mFiFj/Rvi8xy8ZJxgXfRJ9KZX28svbIEIXXVjxqzWduWZVx0ijT3qdigKApBz7e214zg3CRilBSkcb2lvlAJbrRvHJQ0b5EA==', '::1', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/84.0.4147.135 Safari/537.36', '2020-08-29 11:32:13', '1');
INSERT INTO `db_token` VALUES ('2296', '1', 'admin', '9zmG7wuq54l0y+7JRWlFPKeWzpTcqiwN1QhSSKg4QqkAb4C8BmxxySB+u3h/rHQ/gK6X+GHLanVa8ydTmF9ZD/wtD9KuJyfvQFKP+aPryfXQZS81GpNOH5mFiFj/Rvi8xy8ZJxgXfRJ9KZX28svbIEIXXVjxqzWduWZVx0ijT3qdigKApBz7e214zg3CRilBSkcb2lvlAJbrRvHJQ0b5EA==', '::1', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/84.0.4147.135 Safari/537.36', '2020-08-29 12:06:40', '1');
INSERT INTO `db_token` VALUES ('2297', '1', 'admin', '9zmG7wuq54l0y+7JRWlFPKeWzpTcqiwN1QhSSKg4QqkAb4C8BmxxySB+u3h/rHQ/gK6X+GHLanVa8ydTmF9ZD/wtD9KuJyfvQFKP+aPryfXQZS81GpNOH5mFiFj/Rvi8xy8ZJxgXfRJ9KZX28svbIEIXXVjxqzWduWZVx0ijT3qdigKApBz7e214zg3CRilBSkcb2lvlAJbrRvHJQ0b5EA==', '::1', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/84.0.4147.135 Safari/537.36', '2020-08-29 14:29:47', '1');
INSERT INTO `db_token` VALUES ('2307', '1', 'admin', '9zmG7wuq54l0y+7JRWlFPKeWzpTcqiwN1QhSSKg4QqkAb4C8BmxxySB+u3h/rHQ/gK6X+GHLanVa8ydTmF9ZD/wtD9KuJyfvQFKP+aPryfXQZS81GpNOH5mFiFj/Rvi8xy8ZJxgXfRJ9KZX28svbIH096ewImNqpTezgdBOBjU8NC9GEmvW+xSRH5B3VfUi/Skcb2lvlAJbrRvHJQ0b5EA==', '::1', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/76.0.3809.132 Safari/537.36', '2020-08-30 14:37:52', '1');
INSERT INTO `db_token` VALUES ('2309', '1', 'admin', '9zmG7wuq54l0y+7JRWlFPKeWzpTcqiwN1QhSSKg4QqkAb4C8BmxxySB+u3h/rHQ/gK6X+GHLanVa8ydTmF9ZD/wtD9KuJyfvQFKP+aPryfXQZS81GpNOH5mFiFj/Rvi8xy8ZJxgXfRJ9KZX28svbIEIXXVjxqzWduWZVx0ijT3qdigKApBz7e214zg3CRilBSkcb2lvlAJbrRvHJQ0b5EA==', '::1', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/84.0.4147.135 Safari/537.36', '2020-08-30 15:11:21', '1');
INSERT INTO `db_token` VALUES ('2310', '1', 'admin', '9zmG7wuq54l0y+7JRWlFPKeWzpTcqiwN1QhSSKg4QqkAb4C8BmxxySB+u3h/rHQ/6yDQdRSmuyCLNv5Gq+f7dhJr7Fb/yhm5JElj23rOoS8ssmSdFCESYFIa+Zx6I1pLLuMg5Yi5kV+MI11G0EGnB+EHSyIc7kbN2f+knYxe1kHw/e4CDYk8Uhe95N8hN7Ohsj9KvgTBJAb9WSU9wsW851cIy7ThW+X1kCAkfbyhoLvGcB9rgLX1VL5DwoJ/h7Ep', '::1', 'Mozilla/5.0 (iPhone; CPU iPhone OS 13_2_3 like Mac OS X) AppleWebKit/605.1.15 (KHTML, like Gecko) Version/13.0.3 Mobile/15E148 Safari/604.1', '2020-08-30 15:41:40', '1');
INSERT INTO `db_token` VALUES ('2311', '1', 'admin', '9zmG7wuq54l0y+7JRWlFPKeWzpTcqiwN1QhSSKg4QqkAb4C8BmxxySB+u3h/rHQ/gK6X+GHLanVa8ydTmF9ZD/wtD9KuJyfvQFKP+aPryfXQZS81GpNOH5mFiFj/Rvi8xy8ZJxgXfRJ9KZX28svbIEIXXVjxqzWduWZVx0ijT3qdigKApBz7e214zg3CRilBSkcb2lvlAJbrRvHJQ0b5EA==', '::1', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/84.0.4147.135 Safari/537.36', '2020-08-30 15:42:11', '1');
INSERT INTO `db_token` VALUES ('2312', '1', 'admin', '9zmG7wuq54l0y+7JRWlFPKeWzpTcqiwN1QhSSKg4QqkAb4C8BmxxySB+u3h/rHQ/gK6X+GHLanVa8ydTmF9ZD/wtD9KuJyfvQFKP+aPryfXQZS81GpNOH5mFiFj/Rvi8xy8ZJxgXfRJ9KZX28svbIEIXXVjxqzWduWZVx0ijT3qdigKApBz7e214zg3CRilBSkcb2lvlAJbrRvHJQ0b5EA==', '::1', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/84.0.4147.135 Safari/537.36', '2020-08-30 15:44:07', '1');
INSERT INTO `db_token` VALUES ('2313', '1', 'admin', '9zmG7wuq54l0y+7JRWlFPKeWzpTcqiwN1QhSSKg4QqkAb4C8BmxxySB+u3h/rHQ/gK6X+GHLanVa8ydTmF9ZD/wtD9KuJyfvQFKP+aPryfXQZS81GpNOH5mFiFj/Rvi8xy8ZJxgXfRJ9KZX28svbIEIXXVjxqzWduWZVx0ijT3qdigKApBz7e214zg3CRilBSkcb2lvlAJbrRvHJQ0b5EA==', '::1', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/84.0.4147.135 Safari/537.36', '2020-08-30 15:48:13', '1');
INSERT INTO `db_token` VALUES ('2317', '1', 'admin', '9zmG7wuq54l0y+7JRWlFPKeWzpTcqiwN1QhSSKg4QqkAb4C8BmxxySB+u3h/rHQ/gK6X+GHLanVa8ydTmF9ZD/wtD9KuJyfvQFKP+aPryfXQZS81GpNOH5mFiFj/Rvi8xy8ZJxgXfRJ9KZX28svbIEIXXVjxqzWduWZVx0ijT3qdigKApBz7e214zg3CRilBSkcb2lvlAJbrRvHJQ0b5EA==', '::1', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/84.0.4147.135 Safari/537.36', '2020-08-30 17:06:31', '1');
INSERT INTO `db_token` VALUES ('2318', '1', 'admin', '9zmG7wuq54l0y+7JRWlFPKeWzpTcqiwN1QhSSKg4QqkAb4C8BmxxySB+u3h/rHQ/gK6X+GHLanVa8ydTmF9ZD/wtD9KuJyfvQFKP+aPryfXQZS81GpNOH5mFiFj/Rvi8xy8ZJxgXfRJ9KZX28svbIEIXXVjxqzWduWZVx0ijT3qdigKApBz7e214zg3CRilBSkcb2lvlAJbrRvHJQ0b5EA==', '::1', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/84.0.4147.135 Safari/537.36', '2020-08-31 17:38:09', '1');
INSERT INTO `db_token` VALUES ('2322', '1', 'admin', '9zmG7wuq54l0y+7JRWlFPKeWzpTcqiwN1QhSSKg4QqkAb4C8BmxxySB+u3h/rHQ/geu3YXOrdBlXbVXRkPfaM4APtCb4UjCH6/91zqrPPNjROQmKR2FydLaMQTgf8F0hbaOnMjl5wnM8pMdUjFJCnYStfMqyfidiVp7sZ/mJJx9hfT2xrjGjlKfTLkGrZnU6SzYCJHpyorWQi55vsEISAm1A5l+pLUP35YF7BvVYwus=', '::1', 'Mozilla/5.0 (Linux; Android 6.0; Nexus 5 Build/MRA58N) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/76.0.3809.132 Mobile Safari/537.36', '2020-09-03 15:07:03', '1');
INSERT INTO `db_token` VALUES ('2324', '1', 'admin', '9zmG7wuq54l0y+7JRWlFPKeWzpTcqiwN1QhSSKg4QqkAb4C8BmxxySB+u3h/rHQ/gK6X+GHLanVa8ydTmF9ZD/wtD9KuJyfvQFKP+aPryfXQZS81GpNOH5mFiFj/Rvi8xy8ZJxgXfRJ9KZX28svbIH096ewImNqpTezgdBOBjU8NC9GEmvW+xSRH5B3VfUi/Skcb2lvlAJbrRvHJQ0b5EA==', '::1', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/76.0.3809.132 Safari/537.36', '2020-09-03 15:36:42', '1');
INSERT INTO `db_token` VALUES ('2326', '1', 'admin', '9zmG7wuq54l0y+7JRWlFPKKgJPs96uF7owcL8wYBuKnpL63veDNEk0P1gXEb05MBet7QvazKl8C2zT4koRKoTotOfh9CjwQyXN/sW/2kr7E9mR22cmN0pbrm9AdRWMhPVgTfWqgJyZ4ImcJebPiZ5SLYYbIxJ97Ke2x2yvfbEzoKaHf/MBKZsV/fX83eCdOzqYtdjp3LD2AkD9UWBfNqJA==', '127.0.0.1', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/76.0.3809.132 Safari/537.36', '2020-09-03 17:06:12', '1');
INSERT INTO `db_token` VALUES ('2330', '1', 'admin', '9zmG7wuq54l0y+7JRWlFPKeWzpTcqiwN1QhSSKg4QqkAb4C8BmxxySB+u3h/rHQ/gK6X+GHLanVa8ydTmF9ZD/wtD9KuJyfvQFKP+aPryfXQZS81GpNOH5mFiFj/Rvi8xy8ZJxgXfRJ9KZX28svbIH096ewImNqpTezgdBOBjU8NC9GEmvW+xSRH5B3VfUi/Skcb2lvlAJbrRvHJQ0b5EA==', '::1', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/76.0.3809.132 Safari/537.36', '2020-09-03 17:56:18', '1');
INSERT INTO `db_token` VALUES ('2331', '1', 'admin', '9zmG7wuq54l0y+7JRWlFPKeWzpTcqiwN1QhSSKg4QqkAb4C8BmxxySB+u3h/rHQ/gK6X+GHLanVa8ydTmF9ZD/wtD9KuJyfvQFKP+aPryfXQZS81GpNOH5mFiFj/Rvi8xy8ZJxgXfRJ9KZX28svbIH096ewImNqpTezgdBOBjU8NC9GEmvW+xSRH5B3VfUi/Skcb2lvlAJbrRvHJQ0b5EA==', '::1', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/76.0.3809.132 Safari/537.36', '2020-09-04 09:47:18', '1');
INSERT INTO `db_token` VALUES ('2337', '1', 'admin', '9zmG7wuq54l0y+7JRWlFPKeWzpTcqiwN1QhSSKg4QqkAb4C8BmxxySB+u3h/rHQ/gK6X+GHLanVa8ydTmF9ZD/wtD9KuJyfvQFKP+aPryfXQZS81GpNOH5mFiFj/Rvi8xy8ZJxgXfRJ9KZX28svbIH096ewImNqpTezgdBOBjU8NC9GEmvW+xSRH5B3VfUi/Skcb2lvlAJbrRvHJQ0b5EA==', '::1', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/76.0.3809.132 Safari/537.36', '2020-09-05 10:16:27', '1');
INSERT INTO `db_token` VALUES ('2338', '1', 'admin', '9zmG7wuq54l0y+7JRWlFPKeWzpTcqiwN1QhSSKg4QqkAb4C8BmxxySB+u3h/rHQ/gK6X+GHLanVa8ydTmF9ZD/wtD9KuJyfvQFKP+aPryfXQZS81GpNOH5mFiFj/Rvi8xy8ZJxgXfRJ9KZX28svbIH096ewImNqpTezgdBOBjU8NC9GEmvW+xSRH5B3VfUi/Skcb2lvlAJbrRvHJQ0b5EA==', '::1', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/76.0.3809.132 Safari/537.36', '2020-09-05 11:00:51', '1');
INSERT INTO `db_token` VALUES ('2355', '1', 'admin', '9zmG7wuq54l0y+7JRWlFPKeWzpTcqiwN1QhSSKg4QqkAb4C8BmxxySB+u3h/rHQ/gK6X+GHLanVa8ydTmF9ZD/wtD9KuJyfvQFKP+aPryfXQZS81GpNOH5mFiFj/Rvi8xy8ZJxgXfRJ9KZX28svbIOP62lRUxKnjonlGQhudNPUQFoA9iRvUAh/ALrUAtycMbS/T3uje2pDXDWOO/Hjipg==', '::1', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/85.0.4183.83 Safari/537.36', '2020-09-06 14:01:49', '1');
INSERT INTO `db_token` VALUES ('2356', '1', 'admin', '9zmG7wuq54l0y+7JRWlFPKeWzpTcqiwN1QhSSKg4QqkAb4C8BmxxySB+u3h/rHQ/geu3YXOrdBlXbVXRkPfaM4APtCb4UjCH6/91zqrPPNjROQmKR2FydLaMQTgf8F0hbaOnMjl5wnM8pMdUjFJCnYStfMqyfidiVp7sZ/mJJx9hfT2xrjGjlKfTLkGrZnU6SzYCJHpyorWQi55vsEISAm1A5l+pLUP35YF7BvVYwus=', '::1', 'Mozilla/5.0 (Linux; Android 6.0; Nexus 5 Build/MRA58N) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/76.0.3809.132 Mobile Safari/537.36', '2020-09-06 15:34:41', '1');
INSERT INTO `db_token` VALUES ('2365', '1', 'admin', '9zmG7wuq54l0y+7JRWlFPKeWzpTcqiwN1QhSSKg4QqkAb4C8BmxxySB+u3h/rHQ/gK6X+GHLanVa8ydTmF9ZD/wtD9KuJyfvQFKP+aPryfXQZS81GpNOH5mFiFj/Rvi8xy8ZJxgXfRJ9KZX28svbIOP62lRUxKnjonlGQhudNPUQFoA9iRvUAh/ALrUAtycMbS/T3uje2pDXDWOO/Hjipg==', '::1', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/85.0.4183.83 Safari/537.36', '2020-09-10 15:15:23', '1');
INSERT INTO `db_token` VALUES ('2367', '1', 'admin', '9zmG7wuq54l0y+7JRWlFPKeWzpTcqiwN1QhSSKg4QqkAb4C8BmxxySB+u3h/rHQ/gK6X+GHLanVa8ydTmF9ZD/wtD9KuJyfvQFKP+aPryfXQZS81GpNOH5mFiFj/Rvi8xy8ZJxgXfRJ9KZX28svbIOP62lRUxKnjonlGQhudNPUQFoA9iRvUAh/ALrUAtycMbS/T3uje2pDXDWOO/Hjipg==', '::1', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/85.0.4183.83 Safari/537.36', '2020-09-11 00:53:42', '1');
INSERT INTO `db_token` VALUES ('2368', '1', 'admin', '9zmG7wuq54l0y+7JRWlFPKeWzpTcqiwN1QhSSKg4QqkAb4C8BmxxySB+u3h/rHQ/6yDQdRSmuyCLNv5Gq+f7dhJr7Fb/yhm5JElj23rOoS8ssmSdFCESYFIa+Zx6I1pLLuMg5Yi5kV+MI11G0EGnB+EHSyIc7kbN2f+knYxe1kHw/e4CDYk8Uhe95N8hN7Ohsj9KvgTBJAb9WSU9wsW851cIy7ThW+X1kCAkfbyhoLvGcB9rgLX1VL5DwoJ/h7Ep', '::1', 'Mozilla/5.0 (iPhone; CPU iPhone OS 13_2_3 like Mac OS X) AppleWebKit/605.1.15 (KHTML, like Gecko) Version/13.0.3 Mobile/15E148 Safari/604.1', '2020-09-11 01:49:35', '1');
INSERT INTO `db_token` VALUES ('2369', '1', 'admin', '9zmG7wuq54l0y+7JRWlFPKeWzpTcqiwN1QhSSKg4QqkAb4C8BmxxySB+u3h/rHQ/gK6X+GHLanVa8ydTmF9ZD/wtD9KuJyfvQFKP+aPryfXQZS81GpNOH5mFiFj/Rvi8xy8ZJxgXfRJ9KZX28svbIOP62lRUxKnjonlGQhudNPUQFoA9iRvUAh/ALrUAtycMbS/T3uje2pDXDWOO/Hjipg==', '::1', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/85.0.4183.83 Safari/537.36', '2020-09-11 01:50:10', '1');
INSERT INTO `db_token` VALUES ('2374', '1', 'admin', '9zmG7wuq54l0y+7JRWlFPKeWzpTcqiwN1QhSSKg4QqkAb4C8BmxxySB+u3h/rHQ/geu3YXOrdBlXbVXRkPfaM4APtCb4UjCH6/91zqrPPNjROQmKR2FydLaMQTgf8F0hbaOnMjl5wnM8pMdUjFJCnYStfMqyfidiVp7sZ/mJJx9hfT2xrjGjlKfTLkGrZnU6SzYCJHpyorWQi55vsEISAm1A5l+pLUP35YF7BvVYwus=', '::1', 'Mozilla/5.0 (Linux; Android 6.0; Nexus 5 Build/MRA58N) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/76.0.3809.132 Mobile Safari/537.36', '2020-09-13 11:10:29', '1');
INSERT INTO `db_token` VALUES ('2375', '1', 'admin', '9zmG7wuq54l0y+7JRWlFPKeWzpTcqiwN1QhSSKg4QqkAb4C8BmxxySB+u3h/rHQ/gK6X+GHLanVa8ydTmF9ZD/wtD9KuJyfvQFKP+aPryfXQZS81GpNOH5mFiFj/Rvi8xy8ZJxgXfRJ9KZX28svbIH096ewImNqpTezgdBOBjU8NC9GEmvW+xSRH5B3VfUi/Skcb2lvlAJbrRvHJQ0b5EA==', '::1', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/76.0.3809.132 Safari/537.36', '2020-09-13 11:13:50', '1');
INSERT INTO `db_token` VALUES ('2376', '1', 'admin', '9zmG7wuq54l0y+7JRWlFPKeWzpTcqiwN1QhSSKg4QqkAb4C8BmxxySB+u3h/rHQ/geu3YXOrdBlXbVXRkPfaM4APtCb4UjCH6/91zqrPPNjROQmKR2FydLaMQTgf8F0hbaOnMjl5wnM8pMdUjFJCnYStfMqyfidiVp7sZ/mJJx9hfT2xrjGjlKfTLkGrZnU6SzYCJHpyorWQi55vsEISAm1A5l+pLUP35YF7BvVYwus=', '::1', 'Mozilla/5.0 (Linux; Android 6.0; Nexus 5 Build/MRA58N) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/76.0.3809.132 Mobile Safari/537.36', '2020-09-13 11:24:11', '1');
INSERT INTO `db_token` VALUES ('2385', '1', 'admin', '9zmG7wuq54l0y+7JRWlFPKeWzpTcqiwN1QhSSKg4QqkAb4C8BmxxySB+u3h/rHQ/gK6X+GHLanVa8ydTmF9ZD/wtD9KuJyfvQFKP+aPryfXQZS81GpNOH5mFiFj/Rvi8xy8ZJxgXfRJ9KZX28svbIOP62lRUxKnjonlGQhudNPWLW7e7yAjySFApbmLGG6IPSkcb2lvlAJbrRvHJQ0b5EA==', '::1', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/85.0.4183.102 Safari/537.36', '2020-09-17 11:34:43', '1');
INSERT INTO `db_token` VALUES ('2386', '1', 'admin', '9zmG7wuq54l0y+7JRWlFPKeWzpTcqiwN1QhSSKg4QqkAb4C8BmxxySB+u3h/rHQ/gK6X+GHLanVa8ydTmF9ZD/wtD9KuJyfvQFKP+aPryfXQZS81GpNOH5mFiFj/Rvi8xy8ZJxgXfRJ9KZX28svbIOP62lRUxKnjonlGQhudNPWLW7e7yAjySFApbmLGG6IPSkcb2lvlAJbrRvHJQ0b5EA==', '::1', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/85.0.4183.102 Safari/537.36', '2020-09-17 11:35:14', '1');
INSERT INTO `db_token` VALUES ('2404', '1', 'admin', '9zmG7wuq54l0y+7JRWlFPKKgJPs96uF7owcL8wYBuKnpL63veDNEk0P1gXEb05MBet7QvazKl8C2zT4koRKoTotOfh9CjwQyXN/sW/2kr7E9mR22cmN0pbrm9AdRWMhPVgTfWqgJyZ4ImcJebPiZ5SLYYbIxJ97Ke2x2yvfbEzpINEZsHdpCe27QfQZjiDejqYtdjp3LD2AkD9UWBfNqJA==', '127.0.0.1', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/85.0.4183.102 Safari/537.36', '2020-09-19 12:02:33', '1');
INSERT INTO `db_token` VALUES ('2406', '1', 'admin', '9zmG7wuq54l0y+7JRWlFPKeWzpTcqiwN1QhSSKg4QqkAb4C8BmxxySB+u3h/rHQ/gK6X+GHLanVa8ydTmF9ZD/wtD9KuJyfvQFKP+aPryfXQZS81GpNOH5mFiFj/Rvi8xy8ZJxgXfRJ9KZX28svbIOP62lRUxKnjonlGQhudNPWLW7e7yAjySFApbmLGG6IPSkcb2lvlAJbrRvHJQ0b5EA==', '::1', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/85.0.4183.102 Safari/537.36', '2020-09-19 12:07:35', '1');
INSERT INTO `db_token` VALUES ('2408', '1', 'admin', '9zmG7wuq54l0y+7JRWlFPKeWzpTcqiwN1QhSSKg4QqkAb4C8BmxxySB+u3h/rHQ/gK6X+GHLanVa8ydTmF9ZD/wtD9KuJyfvQFKP+aPryfXQZS81GpNOH5mFiFj/Rvi8xy8ZJxgXfRJ9KZX28svbIOP62lRUxKnjonlGQhudNPWLW7e7yAjySFApbmLGG6IPSkcb2lvlAJbrRvHJQ0b5EA==', '::1', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/85.0.4183.102 Safari/537.36', '2020-09-19 13:37:26', '1');
INSERT INTO `db_token` VALUES ('2410', '1', 'admin', '9zmG7wuq54l0y+7JRWlFPKeWzpTcqiwN1QhSSKg4QqkAb4C8BmxxySB+u3h/rHQ/gK6X+GHLanVa8ydTmF9ZD/wtD9KuJyfvQFKP+aPryfXQZS81GpNOH5mFiFj/Rvi8xy8ZJxgXfRJ9KZX28svbIH096ewImNqpTezgdBOBjU8NC9GEmvW+xSRH5B3VfUi/Skcb2lvlAJbrRvHJQ0b5EA==', '::1', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/76.0.3809.132 Safari/537.36', '2020-09-27 11:37:47', '1');
INSERT INTO `db_token` VALUES ('2413', '1', 'admin', '9zmG7wuq54l0y+7JRWlFPKeWzpTcqiwN1QhSSKg4QqkAb4C8BmxxySB+u3h/rHQ/gK6X+GHLanVa8ydTmF9ZD/wtD9KuJyfvQFKP+aPryfXQZS81GpNOH5mFiFj/Rvi8xy8ZJxgXfRJ9KZX28svbIOP62lRUxKnjonlGQhudNPUw8RPrp1yt8pwJr5r7jHExSkcb2lvlAJbrRvHJQ0b5EA==', '::1', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/85.0.4183.121 Safari/537.36', '2020-09-30 11:38:07', '1');
INSERT INTO `db_token` VALUES ('2430', '1', 'admin', '9zmG7wuq54l0y+7JRWlFPKeWzpTcqiwN1QhSSKg4QqkAb4C8BmxxySB+u3h/rHQ/gK6X+GHLanVa8ydTmF9ZD/wtD9KuJyfvQFKP+aPryfXQZS81GpNOH5mFiFj/Rvi8xy8ZJxgXfRJ9KZX28svbIOP62lRUxKnjonlGQhudNPUw8RPrp1yt8pwJr5r7jHExSkcb2lvlAJbrRvHJQ0b5EA==', '::1', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/85.0.4183.121 Safari/537.36', '2020-09-30 18:12:21', '1');
INSERT INTO `db_token` VALUES ('2432', '1', 'admin', '9zmG7wuq54l0y+7JRWlFPKeWzpTcqiwN1QhSSKg4QqkAb4C8BmxxySB+u3h/rHQ/geu3YXOrdBlXbVXRkPfaM4APtCb4UjCH6/91zqrPPNjROQmKR2FydLaMQTgf8F0hbaOnMjl5wnM8pMdUjFJCnYStfMqyfidiVp7sZ/mJJx9hfT2xrjGjlKfTLkGrZnU6SzYCJHpyorWQi55vsEISAm1A5l+pLUP35YF7BvVYwus=', '::1', 'Mozilla/5.0 (Linux; Android 6.0; Nexus 5 Build/MRA58N) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/76.0.3809.132 Mobile Safari/537.36', '2020-10-02 17:28:20', '1');
INSERT INTO `db_token` VALUES ('2434', '1', 'admin', '9zmG7wuq54l0y+7JRWlFPKeWzpTcqiwN1QhSSKg4QqkAb4C8BmxxySB+u3h/rHQ/geu3YXOrdBlXbVXRkPfaM4APtCb4UjCH6/91zqrPPNjROQmKR2FydLaMQTgf8F0hbaOnMjl5wnM8pMdUjFJCnYStfMqyfidiVp7sZ/mJJx9hfT2xrjGjlKfTLkGrZnU6SzYCJHpyorWQi55vsEISAm1A5l+pLUP35YF7BvVYwus=', '::1', 'Mozilla/5.0 (Linux; Android 6.0; Nexus 5 Build/MRA58N) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/76.0.3809.132 Mobile Safari/537.36', '2020-10-02 17:33:53', '1');
INSERT INTO `db_token` VALUES ('2435', '1', 'admin', '9zmG7wuq54l0y+7JRWlFPKeWzpTcqiwN1QhSSKg4QqkAb4C8BmxxySB+u3h/rHQ/gK6X+GHLanVa8ydTmF9ZD/wtD9KuJyfvQFKP+aPryfXQZS81GpNOH5mFiFj/Rvi8xy8ZJxgXfRJ9KZX28svbIOP62lRUxKnjonlGQhudNPUQFoA9iRvUAh/ALrUAtycMbS/T3uje2pDXDWOO/Hjipg==', '::1', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/85.0.4183.83 Safari/537.36', '2020-10-11 03:31:43', '1');
INSERT INTO `db_token` VALUES ('2436', '1', 'admin', 'LNe+yIAQsVo0NyrtEzEmOR7vpgafEDAcAU43IwMHWXzPbWvgK7aaG5dj07/2+f7GjMrHHUqbk80ynbLjDR3mMZNg/Po0tpNtWYQgOpQQXE7MN9C8XSCNpHzmmjgmsVK6D1w8OJ5gIi9eaLx1ouXHVEFV+GPKtQbABWblRIGZuaqtbr9DquV3ao1wl7kInKSU33NMpTfZfq5gj6wtSe84kg==', '::1', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/85.0.4183.121 Safari/537.36', '2020-10-12 12:04:30', '1');
INSERT INTO `db_token` VALUES ('2440', '1', 'admin', 'LNe+yIAQsVo0NyrtEzEmOR7vpgafEDAcAU43IwMHWXzPbWvgK7aaG5dj07/2+f7GjMrHHUqbk80ynbLjDR3mMZNg/Po0tpNtWYQgOpQQXE7MN9C8XSCNpHzmmjgmsVK6D1w8OJ5gIi9eaLx1ouXHVEFV+GPKtQbABWblRIGZuaqtbr9DquV3ao1wl7kInKSU33NMpTfZfq5gj6wtSe84kg==', '::1', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/85.0.4183.121 Safari/537.36', '2020-10-12 14:56:39', '1');
INSERT INTO `db_token` VALUES ('2441', '1', 'admin', 'LNe+yIAQsVo0NyrtEzEmOR7vpgafEDAcAU43IwMHWXzPbWvgK7aaG5dj07/2+f7GjMrHHUqbk80ynbLjDR3mMZNg/Po0tpNtWYQgOpQQXE7MN9C8XSCNpHzmmjgmsVK6D1w8OJ5gIi9eaLx1ouXHVEFV+GPKtQbABWblRIGZuaqtbr9DquV3ao1wl7kInKSU33NMpTfZfq5gj6wtSe84kg==', '::1', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/85.0.4183.121 Safari/537.36', '2020-10-12 15:05:50', '1');
INSERT INTO `db_token` VALUES ('2442', '1', 'admin', 'LNe+yIAQsVo0NyrtEzEmOR7vpgafEDAcAU43IwMHWXzPbWvgK7aaG5dj07/2+f7Gsy4QC9fbnf3XJhDS3GLouuE6VzLc/YMx9+OD6Df44mkCGxtOTYectggi9Pzn+VRar3IvGIE0jCalDGDi7YrLm8PrNSSHXA4fmD1mfsWX+QkxESXTxwYmMRgV51XWRNYdwewajzsJXBDbaFKZ5dILVn6cGBf2BDhHfLzkL0zXBJ5lURKzqhHQCPr3iZfKRBWl', '::1', 'Mozilla/5.0 (iPhone; CPU iPhone OS 13_2_3 like Mac OS X) AppleWebKit/605.1.15 (KHTML, like Gecko) Version/13.0.3 Mobile/15E148 Safari/604.1', '2020-10-12 15:06:22', '1');
INSERT INTO `db_token` VALUES ('2443', '1', 'admin', 'LNe+yIAQsVo0NyrtEzEmOR7vpgafEDAcAU43IwMHWXzPbWvgK7aaG5dj07/2+f7GjMrHHUqbk80ynbLjDR3mMZNg/Po0tpNtWYQgOpQQXE7MN9C8XSCNpHzmmjgmsVK6D1w8OJ5gIi9eaLx1ouXHVEFV+GPKtQbABWblRIGZuaqtbr9DquV3ao1wl7kInKSU33NMpTfZfq5gj6wtSe84kg==', '::1', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/85.0.4183.121 Safari/537.36', '2020-10-12 15:13:58', '1');
INSERT INTO `db_token` VALUES ('2444', '1', 'admin', 'LNe+yIAQsVo0NyrtEzEmOR7vpgafEDAcAU43IwMHWXzPbWvgK7aaG5dj07/2+f7Gsy4QC9fbnf3XJhDS3GLouuE6VzLc/YMx9+OD6Df44mkCGxtOTYectggi9Pzn+VRar3IvGIE0jCalDGDi7YrLm8PrNSSHXA4fmD1mfsWX+QkxESXTxwYmMRgV51XWRNYdwewajzsJXBDbaFKZ5dILVn6cGBf2BDhHfLzkL0zXBJ5lURKzqhHQCPr3iZfKRBWl', '::1', 'Mozilla/5.0 (iPhone; CPU iPhone OS 13_2_3 like Mac OS X) AppleWebKit/605.1.15 (KHTML, like Gecko) Version/13.0.3 Mobile/15E148 Safari/604.1', '2020-10-12 15:18:14', '1');
INSERT INTO `db_token` VALUES ('2445', '1', 'admin', 'LNe+yIAQsVo0NyrtEzEmOR7vpgafEDAcAU43IwMHWXzPbWvgK7aaG5dj07/2+f7GjMrHHUqbk80ynbLjDR3mMZNg/Po0tpNtWYQgOpQQXE7MN9C8XSCNpHzmmjgmsVK6D1w8OJ5gIi9eaLx1ouXHVEFV+GPKtQbABWblRIGZuaqtbr9DquV3ao1wl7kInKSU33NMpTfZfq5gj6wtSe84kg==', '::1', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/85.0.4183.121 Safari/537.36', '2020-10-12 15:20:21', '1');
INSERT INTO `db_token` VALUES ('2447', '1', 'admin', 'LNe+yIAQsVo0NyrtEzEmOR7vpgafEDAcAU43IwMHWXzPbWvgK7aaG5dj07/2+f7GN2L7EVhH01Y42537BgYHv081kzMrjbEzrNM1iLIEfH2thY9m4AbI25oKmBZcmZdECdk+sJfrMQYpnheXJT21gy9fMKn1cXnHE4+Q7jHnQOGyUT2vPw4zmCFrkcqbTgQrg5uzBb1iPTXRdptaYibdOQwbZKti53hx0mFosS62DHw=', '::1', 'Mozilla/5.0 (Linux; Android 6.0; Nexus 5 Build/MRA58N) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/76.0.3809.132 Mobile Safari/537.36', '2020-10-15 10:38:49', '1');
INSERT INTO `db_token` VALUES ('2448', '1', 'admin', 'LNe+yIAQsVo0NyrtEzEmOR7vpgafEDAcAU43IwMHWXzPbWvgK7aaG5dj07/2+f7GjMrHHUqbk80ynbLjDR3mMZNg/Po0tpNtWYQgOpQQXE7MN9C8XSCNpHzmmjgmsVK6D1w8OJ5gIi9eaLx1ouXHVEFV+GPKtQbABWblRIGZuaqtbr9DquV3ao1wl7kInKSU33NMpTfZfq5gj6wtSe84kg==', '::1', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/85.0.4183.121 Safari/537.36', '2020-10-15 11:05:13', '1');
INSERT INTO `db_token` VALUES ('2449', '1', 'admin', 'LNe+yIAQsVo0NyrtEzEmOR7vpgafEDAcAU43IwMHWXzPbWvgK7aaG5dj07/2+f7GjMrHHUqbk80ynbLjDR3mMZNg/Po0tpNtWYQgOpQQXE7MN9C8XSCNpHzmmjgmsVK6D1w8OJ5gIi9eaLx1ouXHVEFV+GPKtQbABWblRIGZuarTarxXzjbS+K+vzlF6zYxYctCwJbv+y0AZOx+VMLC+VQ==', '::1', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/85.0.4183.83 Safari/537.36', '2020-10-15 11:09:05', '1');
INSERT INTO `db_token` VALUES ('2451', '1', 'admin', 'LNe+yIAQsVo0NyrtEzEmOR7vpgafEDAcAU43IwMHWXzPbWvgK7aaG5dj07/2+f7GN2L7EVhH01Y42537BgYHv081kzMrjbEzrNM1iLIEfH2thY9m4AbI25oKmBZcmZdECdk+sJfrMQYpnheXJT21gy9fMKn1cXnHE4+Q7jHnQOGyUT2vPw4zmCFrkcqbTgQrg5uzBb1iPTXRdptaYibdOQwbZKti53hx0mFosS62DHw=', '::1', 'Mozilla/5.0 (Linux; Android 6.0; Nexus 5 Build/MRA58N) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/76.0.3809.132 Mobile Safari/537.36', '2020-10-19 14:24:40', '1');
INSERT INTO `db_token` VALUES ('2453', '1', 'admin', 'LNe+yIAQsVo0NyrtEzEmOR7vpgafEDAcAU43IwMHWXzPbWvgK7aaG5dj07/2+f7GN2L7EVhH01Y42537BgYHv081kzMrjbEzrNM1iLIEfH2thY9m4AbI25oKmBZcmZdECdk+sJfrMQYpnheXJT21gy9fMKn1cXnHE4+Q7jHnQOGyUT2vPw4zmCFrkcqbTgQrg5uzBb1iPTXRdptaYibdOQwbZKti53hx0mFosS62DHw=', '::1', 'Mozilla/5.0 (Linux; Android 6.0; Nexus 5 Build/MRA58N) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/76.0.3809.132 Mobile Safari/537.36', '2020-10-22 14:25:18', '1');
INSERT INTO `db_token` VALUES ('2455', '1', 'admin', 'LNe+yIAQsVo0NyrtEzEmOR7vpgafEDAcAU43IwMHWXzPbWvgK7aaG5dj07/2+f7GN2L7EVhH01Y42537BgYHv081kzMrjbEzrNM1iLIEfH2thY9m4AbI25oKmBZcmZdECdk+sJfrMQYpnheXJT21gy9fMKn1cXnHE4+Q7jHnQOGyUT2vPw4zmCFrkcqbTgQrg5uzBb1iPTXRdptaYibdOQwbZKti53hx0mFosS62DHw=', '::1', 'Mozilla/5.0 (Linux; Android 6.0; Nexus 5 Build/MRA58N) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/76.0.3809.132 Mobile Safari/537.36', '2020-10-22 14:39:54', '1');
INSERT INTO `db_token` VALUES ('2460', '1', 'admin', 'LNe+yIAQsVo0NyrtEzEmOR7vpgafEDAcAU43IwMHWXzPbWvgK7aaG5dj07/2+f7GjMrHHUqbk80ynbLjDR3mMZNg/Po0tpNtWYQgOpQQXE7MN9C8XSCNpHzmmjgmsVK6D1w8OJ5gIi9eaLx1ouXHVEFV+GPKtQbABWblRIGZuaqtbr9DquV3ao1wl7kInKSU33NMpTfZfq5gj6wtSe84kg==', '::1', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/85.0.4183.121 Safari/537.36', '2020-10-24 10:23:05', '1');
INSERT INTO `db_token` VALUES ('2463', '1', 'admin', 'LNe+yIAQsVo0NyrtEzEmOR7vpgafEDAcAU43IwMHWXzPbWvgK7aaG5dj07/2+f7GN2L7EVhH01Y42537BgYHv081kzMrjbEzrNM1iLIEfH2thY9m4AbI25oKmBZcmZdECdk+sJfrMQYpnheXJT21gy9fMKn1cXnHE4+Q7jHnQOGyUT2vPw4zmCFrkcqbTgQrg5uzBb1iPTXRdptaYibdOQwbZKti53hx0mFosS62DHw=', '::1', 'Mozilla/5.0 (Linux; Android 6.0; Nexus 5 Build/MRA58N) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/76.0.3809.132 Mobile Safari/537.36', '2020-10-24 18:11:00', '1');
INSERT INTO `db_token` VALUES ('2465', '1', 'admin', 'LNe+yIAQsVo0NyrtEzEmOR7vpgafEDAcAU43IwMHWXzPbWvgK7aaG5dj07/2+f7GN2L7EVhH01Y42537BgYHv081kzMrjbEzrNM1iLIEfH2thY9m4AbI25oKmBZcmZdECdk+sJfrMQYpnheXJT21gy9fMKn1cXnHE4+Q7jHnQOGyUT2vPw4zmCFrkcqbTgQrg5uzBb1iPTXRdptaYibdOQwbZKti53hx0mFosS62DHw=', '::1', 'Mozilla/5.0 (Linux; Android 6.0; Nexus 5 Build/MRA58N) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/76.0.3809.132 Mobile Safari/537.36', '2020-10-25 12:21:03', '1');
INSERT INTO `db_token` VALUES ('2467', '1', 'admin', 'LNe+yIAQsVo0NyrtEzEmOR7vpgafEDAcAU43IwMHWXzPbWvgK7aaG5dj07/2+f7GN2L7EVhH01Y42537BgYHv081kzMrjbEzrNM1iLIEfH2thY9m4AbI25oKmBZcmZdECdk+sJfrMQYpnheXJT21gy9fMKn1cXnHE4+Q7jHnQOGyUT2vPw4zmCFrkcqbTgQrz0u59S+E4WalCyTrjrpqnAwbZKti53hx0mFosS62DHw=', '::1', 'Mozilla/5.0 (Linux; Android 6.0; Nexus 5 Build/MRA58N) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/76.0.3809.100 Mobile Safari/537.36', '2020-10-25 18:16:39', '1');
INSERT INTO `db_token` VALUES ('2469', '1', 'admin', 'LNe+yIAQsVo0NyrtEzEmOR7vpgafEDAcAU43IwMHWXzPbWvgK7aaG5dj07/2+f7GN2L7EVhH01Y42537BgYHv081kzMrjbEzrNM1iLIEfH2thY9m4AbI25oKmBZcmZdECdk+sJfrMQYpnheXJT21gy9fMKn1cXnHE4+Q7jHnQOGyUT2vPw4zmCFrkcqbTgQrz0u59S+E4WalCyTrjrpqnAwbZKti53hx0mFosS62DHw=', '::1', 'Mozilla/5.0 (Linux; Android 6.0; Nexus 5 Build/MRA58N) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/76.0.3809.100 Mobile Safari/537.36', '2020-10-26 10:20:28', '1');
INSERT INTO `db_token` VALUES ('2478', '1', 'admin', 'LNe+yIAQsVo0NyrtEzEmOR7vpgafEDAcAU43IwMHWXzPbWvgK7aaG5dj07/2+f7GjMrHHUqbk80ynbLjDR3mMZNg/Po0tpNtWYQgOpQQXE7MN9C8XSCNpHzmmjgmsVK6D1w8OJ5gIi9eaLx1ouXHVHzojdSm8JTm1vudc4Sfa4oDyJUr7siwjKeNy56ykQ0c33NMpTfZfq5gj6wtSe84kg==', '::1', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/86.0.4240.111 Safari/537.36', '2020-10-26 14:10:32', '1');
INSERT INTO `db_token` VALUES ('2494', '1', 'admin', 'DSpazZTPAw74/cO3CGcMWCQXt+sVyov5PHBfsOl5DnnPbWvgK7aaG5dj07/2+f7GjMrHHUqbk80ynbLjDR3mMZNg/Po0tpNtWYQgOpQQXE7MN9C8XSCNpHzmmjgmsVK6D1w8OJ5gIi9eaLx1ouXHVEFV+GPKtQbABWblRIGZuarTarxXzjbS+K+vzlF6zYxYctCwJbv+y0AZOx+VMLC+VQ==', '::1', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/85.0.4183.83 Safari/537.36', '2020-10-28 22:28:13', '1');
INSERT INTO `db_token` VALUES ('2496', '1', 'admin', 'DSpazZTPAw74/cO3CGcMWCQXt+sVyov5PHBfsOl5DnnPbWvgK7aaG5dj07/2+f7GN2L7EVhH01Y42537BgYHv081kzMrjbEzrNM1iLIEfH2thY9m4AbI25oKmBZcmZdECdk+sJfrMQYpnheXJT21gy9fMKn1cXnHE4+Q7jHnQOGyUT2vPw4zmCFrkcqbTgQrg5uzBb1iPTXRdptaYibdOQwbZKti53hx0mFosS62DHw=', '::1', 'Mozilla/5.0 (Linux; Android 6.0; Nexus 5 Build/MRA58N) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/76.0.3809.132 Mobile Safari/537.36', '2020-10-29 10:27:07', '1');
INSERT INTO `db_token` VALUES ('2498', '1', 'admin', 'DSpazZTPAw74/cO3CGcMWCQXt+sVyov5PHBfsOl5DnnPbWvgK7aaG5dj07/2+f7GN2L7EVhH01Y42537BgYHv081kzMrjbEzrNM1iLIEfH2thY9m4AbI25oKmBZcmZdECdk+sJfrMQYpnheXJT21gy9fMKn1cXnHE4+Q7jHnQOGyUT2vPw4zmCFrkcqbTgQrg5uzBb1iPTXRdptaYibdOQwbZKti53hx0mFosS62DHw=', '::1', 'Mozilla/5.0 (Linux; Android 6.0; Nexus 5 Build/MRA58N) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/76.0.3809.132 Mobile Safari/537.36', '2020-10-29 11:10:37', '1');
INSERT INTO `db_token` VALUES ('2500', '1', 'admin', 'DSpazZTPAw74/cO3CGcMWCQXt+sVyov5PHBfsOl5DnnPbWvgK7aaG5dj07/2+f7GjMrHHUqbk80ynbLjDR3mMZNg/Po0tpNtWYQgOpQQXE7MN9C8XSCNpHzmmjgmsVK6D1w8OJ5gIi9eaLx1ouXHVEFV+GPKtQbABWblRIGZuarTarxXzjbS+K+vzlF6zYxYctCwJbv+y0AZOx+VMLC+VQ==', '::1', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/85.0.4183.83 Safari/537.36', '2020-10-29 12:25:58', '1');
INSERT INTO `db_token` VALUES ('2505', '1', 'admin', 'DSpazZTPAw74/cO3CGcMWCQXt+sVyov5PHBfsOl5DnnPbWvgK7aaG5dj07/2+f7GN2L7EVhH01Y42537BgYHv081kzMrjbEzrNM1iLIEfH2thY9m4AbI25oKmBZcmZdECdk+sJfrMQYpnheXJT21gy9fMKn1cXnHE4+Q7jHnQOGyUT2vPw4zmCFrkcqbTgQrg5uzBb1iPTXRdptaYibdOQwbZKti53hx0mFosS62DHw=', '::1', 'Mozilla/5.0 (Linux; Android 6.0; Nexus 5 Build/MRA58N) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/76.0.3809.132 Mobile Safari/537.36', '2020-10-29 16:43:44', '1');
INSERT INTO `db_token` VALUES ('2509', '1', 'admin', 'DSpazZTPAw74/cO3CGcMWOe6s2TFv/uP93EC8N0msfV01/71jmIp32gk9KNhWRopWJ4CrYnL/0EsIB//ZgulDmaWsjdJTKKUA5OHX+7lmP5Kfn1IJNeng3fKDqX0rhoNYvFrwC+qfSQmvAV77J8M+ez8l4mqaVZVuu4gd05hg3lfxyDAl66YAgfpQTkOtfyMvi820SS+nK4n6tHBYk2nkyymr3ujYWxCohm3aUlBDRMfqmefZ0Ch1LcSKpSClJAQ', '222.216.19.43', 'Mozilla/5.0 (Linux; Android 6.0; Nexus 5 Build/MRA58N) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/76.0.3809.132 Mobile Safari/537.36', '2020-10-29 17:51:52', '1');
INSERT INTO `db_token` VALUES ('2516', '1', 'admin', 'DSpazZTPAw74/cO3CGcMWCQXt+sVyov5PHBfsOl5DnnPbWvgK7aaG5dj07/2+f7GN2L7EVhH01Y42537BgYHv081kzMrjbEzrNM1iLIEfH2thY9m4AbI25oKmBZcmZdECdk+sJfrMQYpnheXJT21gy9fMKn1cXnHE4+Q7jHnQOGyUT2vPw4zmCFrkcqbTgQrg5uzBb1iPTXRdptaYibdOQwbZKti53hx0mFosS62DHw=', '::1', 'Mozilla/5.0 (Linux; Android 6.0; Nexus 5 Build/MRA58N) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/76.0.3809.132 Mobile Safari/537.36', '2020-10-29 18:07:28', '1');
INSERT INTO `db_token` VALUES ('2517', '1', 'admin', 'DSpazZTPAw74/cO3CGcMWOe6s2TFv/uP93EC8N0msfV01/71jmIp32gk9KNhWRopajLfPu7J48lIiBVT/33qCG74BGKZu0O2e1xTlHxdtqdP+m9MBe0dlJcEgkQvxKQTaYfl+PeQIrAXNjFIKw7VWhGtIU6CUf/hxfngO9ateyHoL9yA2qofY4rb/676qE7Z8NaX7RYPPg9n2FXyU9hboMw8jJcZNpO/DYd6sVyaofs=', '222.216.19.43', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/86.0.4240.111 Safari/537.36', '2020-10-29 18:10:01', '1');
INSERT INTO `db_token` VALUES ('2518', '1', 'admin', 'DSpazZTPAw74/cO3CGcMWOe6s2TFv/uP93EC8N0msfV01/71jmIp32gk9KNhWRopajLfPu7J48lIiBVT/33qCG74BGKZu0O2e1xTlHxdtqdP+m9MBe0dlJcEgkQvxKQTaYfl+PeQIrAXNjFIKw7VWhGtIU6CUf/hxfngO9ateyF59MvJTX7U0MSxEgO4f1Avd6ZazmRVRB0WdmzprOZgPVeNNXyd7MzaARypzHTtxqk=', '222.216.19.43', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/85.0.4183.83 Safari/537.36', '2020-10-29 18:11:46', '1');
INSERT INTO `db_token` VALUES ('2532', '1', 'admin', 'DSpazZTPAw74/cO3CGcMWCQXt+sVyov5PHBfsOl5DnnPbWvgK7aaG5dj07/2+f7GjMrHHUqbk80ynbLjDR3mMZNg/Po0tpNtWYQgOpQQXE7MN9C8XSCNpHzmmjgmsVK6D1w8OJ5gIi9eaLx1ouXHVHzojdSm8JTm1vudc4Sfa4oDyJUr7siwjKeNy56ykQ0c33NMpTfZfq5gj6wtSe84kg==', '::1', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/86.0.4240.111 Safari/537.36', '2020-10-30 17:03:07', '1');
INSERT INTO `db_token` VALUES ('2533', '1', 'admin', 'DSpazZTPAw74/cO3CGcMWLB2EB/3rQ5xzMwD6Wh7RCOkP5sxPwTDn+hcVLqVuMa9CnZ87m0DlLkHxDDBUQ5zQDGC4QunO76cObJm4MewneMCtTJ3yRrd8TnyI9rTaWU8fWu9rKRppzqbM9nWkB3gXhDK/SysMzC14raYxTfcaqrw5YGjgy71jRy2GqCxQaLJEGrvy2v62nGCaANkWtNT+WJU+1U09HtxLzYTH8SpeQE=', '116.252.134.189', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/85.0.4183.83 Safari/537.36', '2020-10-30 18:15:29', '1');
INSERT INTO `db_token` VALUES ('2535', '1', 'admin', 'DSpazZTPAw74/cO3CGcMWLB2EB/3rQ5xzMwD6Wh7RCOkP5sxPwTDn+hcVLqVuMa9bqBdvdIQwx2jQGmrZRPKwwNVcn3s449IUl3T8vO2o7nAFTKdtzBgaitwnBFmZ5VDDlPw8eLsk4kbJmR6vOKY7y0aiON3De31+u4C4E4fQDU7+oroFOcUZL2Dy2Zz5WYZSfAtZOh7MCIJTCJtlEUXa+WCvanwwnpCzaenOdZdFjDfc0ylN9l+rmCPrC1J7ziS', '116.252.134.189', 'Mozilla/5.0 (Linux; Android 6.0; Nexus 5 Build/MRA58N) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/76.0.3809.132 Mobile Safari/537.36', '2020-10-30 18:17:19', '1');
INSERT INTO `db_token` VALUES ('2536', '1', 'admin', 'DSpazZTPAw74/cO3CGcMWCQXt+sVyov5PHBfsOl5DnnPbWvgK7aaG5dj07/2+f7GN2L7EVhH01Y42537BgYHv081kzMrjbEzrNM1iLIEfH2thY9m4AbI25oKmBZcmZdECdk+sJfrMQYpnheXJT21gy9fMKn1cXnHE4+Q7jHnQOGyUT2vPw4zmCFrkcqbTgQrg5uzBb1iPTXRdptaYibdOQwbZKti53hx0mFosS62DHw=', '::1', 'Mozilla/5.0 (Linux; Android 6.0; Nexus 5 Build/MRA58N) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/76.0.3809.132 Mobile Safari/537.36', '2020-10-30 18:31:05', '1');
INSERT INTO `db_token` VALUES ('2537', '1', 'admin', 'DSpazZTPAw74/cO3CGcMWLB2EB/3rQ5xzMwD6Wh7RCOkP5sxPwTDn+hcVLqVuMa9CnZ87m0DlLkHxDDBUQ5zQDGC4QunO76cObJm4MewneMCtTJ3yRrd8TnyI9rTaWU8fWu9rKRppzqbM9nWkB3gXhDK/SysMzC14raYxTfcaqqqTXcleykdrxQHt8dfTJX1wZi5Ge84vzMSriRqlED1WJtGI1B0hxOKO5cdkekZcew=', '116.252.134.189', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/86.0.4240.111 Safari/537.36', '2020-10-30 18:37:32', '1');
INSERT INTO `db_token` VALUES ('2539', '1', 'admin', 'DSpazZTPAw74/cO3CGcMWCQXt+sVyov5PHBfsOl5DnnPbWvgK7aaG5dj07/2+f7GN2L7EVhH01Y42537BgYHv081kzMrjbEzrNM1iLIEfH2thY9m4AbI25oKmBZcmZdECdk+sJfrMQYpnheXJT21gy9fMKn1cXnHE4+Q7jHnQOGyUT2vPw4zmCFrkcqbTgQrz0u59S+E4WalCyTrjrpqnAwbZKti53hx0mFosS62DHw=', '::1', 'Mozilla/5.0 (Linux; Android 6.0; Nexus 5 Build/MRA58N) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/76.0.3809.100 Mobile Safari/537.36', '2020-10-30 21:12:09', '1');
INSERT INTO `db_token` VALUES ('2540', '1', 'admin', 'DSpazZTPAw74/cO3CGcMWCQXt+sVyov5PHBfsOl5DnnPbWvgK7aaG5dj07/2+f7GN2L7EVhH01Y42537BgYHv081kzMrjbEzrNM1iLIEfH2thY9m4AbI25oKmBZcmZdECdk+sJfrMQYpnheXJT21gy9fMKn1cXnHE4+Q7jHnQOGyUT2vPw4zmCFrkcqbTgQrg5uzBb1iPTXRdptaYibdOQwbZKti53hx0mFosS62DHw=', '::1', 'Mozilla/5.0 (Linux; Android 6.0; Nexus 5 Build/MRA58N) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/76.0.3809.132 Mobile Safari/537.36', '2020-10-31 09:21:28', '1');
INSERT INTO `db_token` VALUES ('2541', '1', 'admin', 'DSpazZTPAw74/cO3CGcMWLB2EB/3rQ5xzMwD6Wh7RCOkP5sxPwTDn+hcVLqVuMa9CnZ87m0DlLkHxDDBUQ5zQDGC4QunO76cObJm4MewneMCtTJ3yRrd8TnyI9rTaWU8fWu9rKRppzqbM9nWkB3gXhDK/SysMzC14raYxTfcaqqqTXcleykdrxQHt8dfTJX1wZi5Ge84vzMSriRqlED1WJtGI1B0hxOKO5cdkekZcew=', '116.252.134.189', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/86.0.4240.111 Safari/537.36', '2020-10-31 10:47:21', '1');
INSERT INTO `db_token` VALUES ('2543', '1', 'admin', 'DSpazZTPAw74/cO3CGcMWEa6fm7XWdRopugTzaVTMTwP4kzwGjvPbxRZI9v2T+/GCnZ87m0DlLkHxDDBUQ5zQDGC4QunO76cObJm4MewneMCtTJ3yRrd8TnyI9rTaWU8fWu9rKRppzqbM9nWkB3gXhDK/SysMzC14raYxTfcaqqqTXcleykdrxQHt8dfTJX1wZi5Ge84vzMSriRqlED1WJtGI1B0hxOKO5cdkekZcew=', '180.136.225.139', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/86.0.4240.111 Safari/537.36', '2020-10-31 14:13:14', '1');
INSERT INTO `db_token` VALUES ('2545', '1', 'admin', 'DSpazZTPAw74/cO3CGcMWEa6fm7XWdRopugTzaVTMTwP4kzwGjvPbxRZI9v2T+/GCnZ87m0DlLkHxDDBUQ5zQDGC4QunO76cObJm4MewneMCtTJ3yRrd8TnyI9rTaWU8fWu9rKRppzqbM9nWkB3gXhDK/SysMzC14raYxTfcaqqqTXcleykdrxQHt8dfTJX1wZi5Ge84vzMSriRqlED1WJtGI1B0hxOKO5cdkekZcew=', '180.136.225.139', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/86.0.4240.111 Safari/537.36', '2020-10-31 14:17:11', '1');
INSERT INTO `db_token` VALUES ('2548', '1', 'admin', 'DSpazZTPAw74/cO3CGcMWCQXt+sVyov5PHBfsOl5DnnPbWvgK7aaG5dj07/2+f7GjMrHHUqbk80ynbLjDR3mMZNg/Po0tpNtWYQgOpQQXE7MN9C8XSCNpHzmmjgmsVK6D1w8OJ5gIi9eaLx1ouXHVHzojdSm8JTm1vudc4Sfa4oDyJUr7siwjKeNy56ykQ0c33NMpTfZfq5gj6wtSe84kg==', '::1', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/86.0.4240.111 Safari/537.36', '2020-10-31 15:24:02', '1');
INSERT INTO `db_token` VALUES ('2549', '1', 'admin', 'DSpazZTPAw74/cO3CGcMWEa6fm7XWdRopugTzaVTMTwP4kzwGjvPbxRZI9v2T+/GCnZ87m0DlLkHxDDBUQ5zQDGC4QunO76cObJm4MewneMCtTJ3yRrd8TnyI9rTaWU8fWu9rKRppzqbM9nWkB3gXhDK/SysMzC14raYxTfcaqqqTXcleykdrxQHt8dfTJX1wZi5Ge84vzMSriRqlED1WJtGI1B0hxOKO5cdkekZcew=', '180.136.225.139', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/86.0.4240.111 Safari/537.36', '2020-10-31 15:28:46', '1');
INSERT INTO `db_token` VALUES ('2550', '1', 'admin', 'DSpazZTPAw74/cO3CGcMWCQXt+sVyov5PHBfsOl5DnnPbWvgK7aaG5dj07/2+f7GjMrHHUqbk80ynbLjDR3mMZNg/Po0tpNtWYQgOpQQXE7MN9C8XSCNpHzmmjgmsVK6D1w8OJ5gIi9eaLx1ouXHVHzojdSm8JTm1vudc4Sfa4oDyJUr7siwjKeNy56ykQ0c33NMpTfZfq5gj6wtSe84kg==', '::1', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/86.0.4240.111 Safari/537.36', '2020-10-31 15:41:53', '1');
INSERT INTO `db_token` VALUES ('2553', '1', 'admin', 'DSpazZTPAw74/cO3CGcMWEa6fm7XWdRopugTzaVTMTwP4kzwGjvPbxRZI9v2T+/GCnZ87m0DlLkHxDDBUQ5zQDGC4QunO76cObJm4MewneMCtTJ3yRrd8TnyI9rTaWU8fWu9rKRppzqbM9nWkB3gXhDK/SysMzC14raYxTfcaqqqTXcleykdrxQHt8dfTJX1wZi5Ge84vzMSriRqlED1WJtGI1B0hxOKO5cdkekZcew=', '180.136.225.139', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/86.0.4240.111 Safari/537.36', '2020-11-02 10:20:42', '1');
INSERT INTO `db_token` VALUES ('2554', '1', 'admin', 'DSpazZTPAw74/cO3CGcMWPdUBOWl17CfO2iy0tycuKCMc0bln1mIQiumLd3X4fIseVJfFIctEc5WpPadGkFHQUmHw3CPXNu5hLJL+Z02v8nenfOsILcB2GNNcNBW5tgrZfsnryf3sHFxFbwP1fjC/thGvwBW0IoAR09HmnwpUYwODkAptjMkoSctS2+aX3f4kfEV4B6O+l2QYpiHYCgRq9gkz6aw9pMwma+ETS+bMWxiVPtVNPR7cS82Ex/EqXkB', '117.140.132.92', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/86.0.4240.111 Safari/537.36 Edg/86.0.622.56', '2020-11-02 20:05:11', '1');
INSERT INTO `db_token` VALUES ('2555', '1', 'admin', 'DSpazZTPAw74/cO3CGcMWOe6s2TFv/uP93EC8N0msfUm4m0vUJMZYb/S5FdV3OYSbqBdvdIQwx2jQGmrZRPKwxdAKDwDinq2fW3Zncn4iw/2wQu1H3qXXUJ4NjcgWIeI1klU4BDK8ytLBr3HZscfDGa31j/kJ+FITU9tjx46js0J2T6wl+sxBimeF5clPbWDL18wqfVxeccTj5DuMedA4d3zBclgMbSYZvAPNvzsa7F2KEK2H5auOfBP/1r4tOg9hZTceSxb2G0YD8hat/FwqnsuM8meYAhVXkwMxRdPktLfc0ylN9l+rmCPrC1J7ziS', '222.216.162.162', 'Mozilla/5.0 (Linux; U; Android 9; zh-cn; vivo NEX A Build/PKQ1.181030.001) AppleWebKit/537.36 (KHTML, like Gecko) Version/4.0 Chrome/66.0.3359.126 MQQBrowser/10.9 Mobile Safari/537.36', '2020-11-02 20:17:11', '1');
INSERT INTO `db_token` VALUES ('2556', '1', 'admin', 'DSpazZTPAw74/cO3CGcMWPdUBOWl17CfO2iy0tycuKCMc0bln1mIQiumLd3X4fIseVJfFIctEc5WpPadGkFHQUmHw3CPXNu5hLJL+Z02v8nenfOsILcB2GNNcNBW5tgrZfsnryf3sHFxFbwP1fjC/thGvwBW0IoAR09HmnwpUYwODkAptjMkoSctS2+aX3f4kfEV4B6O+l2QYpiHYCgRq9gkz6aw9pMwma+ETS+bMWxiVPtVNPR7cS82Ex/EqXkB', '117.140.132.92', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/86.0.4240.111 Safari/537.36 Edg/86.0.622.56', '2020-11-03 12:26:44', '1');
INSERT INTO `db_token` VALUES ('2557', '1', 'admin', 'DSpazZTPAw74/cO3CGcMWI1XSrMoX/mbSkVxRLVy/nBVChMUKSd+CxTIHroDtXYSWJ4CrYnL/0EsIB//ZgulDqNAZv3bBvzYKEKMvG41MrvqaEX+p983DzcaNABJVFAbZfsnryf3sHFxFbwP1fjC/thGvwBW0IoAR09HmnwpUYygSbW9svzLGp7a4/kFA5Fc1NbeQMRN7taP71JUPWqDIxbjZwNGzA4GvvOFgpYgirU=', '171.104.2.178', 'Mozilla/5.0 (Linux; Android 9; vivo NEX A) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/76.0.3809.132 Mobile Safari/537.36', '2020-11-03 13:26:31', '1');
INSERT INTO `db_token` VALUES ('2559', '1', 'admin', 'DSpazZTPAw74/cO3CGcMWIcYpNe3/lX0kY4XnR9OjQ9jS64H+YiiwmEQ5VEO+UcLajLfPu7J48lIiBVT/33qCG74BGKZu0O2e1xTlHxdtqdP+m9MBe0dlJcEgkQvxKQTaYfl+PeQIrAXNjFIKw7VWhGtIU6CUf/hxfngO9ateyHoL9yA2qofY4rb/676qE7Z8NaX7RYPPg9n2FXyU9hboMw8jJcZNpO/DYd6sVyaofs=', '180.141.36.24', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/86.0.4240.111 Safari/537.36', '2020-11-05 18:11:05', '1');
INSERT INTO `db_token` VALUES ('2561', '1', 'admin', 'DSpazZTPAw74/cO3CGcMWCQXt+sVyov5PHBfsOl5DnnPbWvgK7aaG5dj07/2+f7GN2L7EVhH01Y42537BgYHv081kzMrjbEzrNM1iLIEfH2thY9m4AbI25oKmBZcmZdECdk+sJfrMQYpnheXJT21gy9fMKn1cXnHE4+Q7jHnQOGyUT2vPw4zmCFrkcqbTgQrz0u59S+E4WalCyTrjrpqnAwbZKti53hx0mFosS62DHw=', '::1', 'Mozilla/5.0 (Linux; Android 6.0; Nexus 5 Build/MRA58N) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/76.0.3809.100 Mobile Safari/537.36', '2020-11-09 09:48:21', '1');
INSERT INTO `db_token` VALUES ('2563', '1', 'admin', 'DSpazZTPAw74/cO3CGcMWCQXt+sVyov5PHBfsOl5DnnPbWvgK7aaG5dj07/2+f7GN2L7EVhH01Y42537BgYHv081kzMrjbEzrNM1iLIEfH2thY9m4AbI25oKmBZcmZdECdk+sJfrMQYpnheXJT21gy9fMKn1cXnHE4+Q7jHnQOGyUT2vPw4zmCFrkcqbTgQrg5uzBb1iPTXRdptaYibdOQwbZKti53hx0mFosS62DHw=', '::1', 'Mozilla/5.0 (Linux; Android 6.0; Nexus 5 Build/MRA58N) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/76.0.3809.132 Mobile Safari/537.36', '2020-11-12 14:37:25', '1');
INSERT INTO `db_token` VALUES ('2566', '1', 'admin', 'DSpazZTPAw74/cO3CGcMWCQXt+sVyov5PHBfsOl5DnnPbWvgK7aaG5dj07/2+f7GjMrHHUqbk80ynbLjDR3mMZNg/Po0tpNtWYQgOpQQXE7MN9C8XSCNpHzmmjgmsVK6D1w8OJ5gIi9eaLx1ouXHVHzojdSm8JTm1vudc4Sfa4oPcGbmWTH6PqdC3m/DTA9w33NMpTfZfq5gj6wtSe84kg==', '::1', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/86.0.4240.198 Safari/537.36', '2020-11-22 12:11:27', '1');
INSERT INTO `db_token` VALUES ('2567', '1', 'admin', 'DSpazZTPAw74/cO3CGcMWCQXt+sVyov5PHBfsOl5DnnPbWvgK7aaG5dj07/2+f7GjMrHHUqbk80ynbLjDR3mMZNg/Po0tpNtWYQgOpQQXE7MN9C8XSCNpHzmmjgmsVK6D1w8OJ5gIi9eaLx1ouXHVFGLHDx7kQHGeN+QGF8EDq+1fn9iiaGGkR/64RxnCh/rctCwJbv+y0AZOx+VMLC+VQ==', '::1', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/87.0.4280.66 Safari/537.36', '2020-12-04 14:41:38', '1');
INSERT INTO `db_token` VALUES ('2591', '1', 'admin', 'DSpazZTPAw74/cO3CGcMWCQXt+sVyov5PHBfsOl5DnnPbWvgK7aaG5dj07/2+f7GjMrHHUqbk80ynbLjDR3mMZNg/Po0tpNtWYQgOpQQXE7MN9C8XSCNpHzmmjgmsVK6D1w8OJ5gIi9eaLx1ouXHVFGLHDx7kQHGeN+QGF8EDq+pTEJX/LTHXEDCXiELyhqgctCwJbv+y0AZOx+VMLC+VQ==', '::1', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/87.0.4280.88 Safari/537.36', '2020-12-17 13:46:00', '1');
INSERT INTO `db_token` VALUES ('2592', '1', 'admin', 'DSpazZTPAw74/cO3CGcMWCQXt+sVyov5PHBfsOl5DnnPbWvgK7aaG5dj07/2+f7GjMrHHUqbk80ynbLjDR3mMZNg/Po0tpNtWYQgOpQQXE7MN9C8XSCNpHzmmjgmsVK6D1w8OJ5gIi9eaLx1ouXHVFGLHDx7kQHGeN+QGF8EDq+pTEJX/LTHXEDCXiELyhqgctCwJbv+y0AZOx+VMLC+VQ==', '::1', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/87.0.4280.88 Safari/537.36', '2020-12-17 13:46:50', '1');
INSERT INTO `db_token` VALUES ('2595', '1', 'admin', 'DSpazZTPAw74/cO3CGcMWCQXt+sVyov5PHBfsOl5DnnPbWvgK7aaG5dj07/2+f7GjMrHHUqbk80ynbLjDR3mMZNg/Po0tpNtWYQgOpQQXE7MN9C8XSCNpHzmmjgmsVK6D1w8OJ5gIi9eaLx1ouXHVFGLHDx7kQHGeN+QGF8EDq+pTEJX/LTHXEDCXiELyhqgctCwJbv+y0AZOx+VMLC+VQ==', '::1', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/87.0.4280.88 Safari/537.36', '2020-12-17 15:50:20', '1');
INSERT INTO `db_token` VALUES ('2612', '1', 'admin', 'DSpazZTPAw74/cO3CGcMWCQXt+sVyov5PHBfsOl5DnnPbWvgK7aaG5dj07/2+f7GjMrHHUqbk80ynbLjDR3mMZNg/Po0tpNtWYQgOpQQXE7MN9C8XSCNpHzmmjgmsVK6D1w8OJ5gIi9eaLx1ouXHVKgmaOkn8y7g81SAxWp9IHLrS1l9hG+JGtpPDCh7lxJE33NMpTfZfq5gj6wtSe84kg==', '::1', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/76.0.3809.132 Safari/537.36', '2020-12-18 10:31:39', '1');
INSERT INTO `db_token` VALUES ('2646', '1', 'admin', 'DSpazZTPAw74/cO3CGcMWCQXt+sVyov5PHBfsOl5DnnPbWvgK7aaG5dj07/2+f7GjMrHHUqbk80ynbLjDR3mMZNg/Po0tpNtWYQgOpQQXE7MN9C8XSCNpHzmmjgmsVK6D1w8OJ5gIi9eaLx1ouXHVFGLHDx7kQHGeN+QGF8EDq+pTEJX/LTHXEDCXiELyhqgctCwJbv+y0AZOx+VMLC+VQ==', '::1', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/87.0.4280.88 Safari/537.36', '2020-12-18 15:21:03', '1');
INSERT INTO `db_token` VALUES ('2647', '1', 'admin', 'DSpazZTPAw74/cO3CGcMWI1XSrMoX/mbSkVxRLVy/nBKFJhGpH1Rv2w6VFtprpJ9CnZ87m0DlLkHxDDBUQ5zQDGC4QunO76cObJm4MewneMCtTJ3yRrd8TnyI9rTaWU8fWu9rKRppzqbM9nWkB3gXhDK/SysMzC14raYxTfcaqpND2PIuCgYqGVLwGi1mDYx2rjl5rqVxxkMXEey0JsgQGJU+1U09HtxLzYTH8SpeQE=', '171.104.223.179', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/87.0.4280.88 Safari/537.36', '2020-12-18 16:07:00', '1');
INSERT INTO `db_token` VALUES ('2650', '1', 'admin', 'DSpazZTPAw74/cO3CGcMWI1XSrMoX/mbSkVxRLVy/nBKFJhGpH1Rv2w6VFtprpJ9CnZ87m0DlLkHxDDBUQ5zQDGC4QunO76cObJm4MewneMCtTJ3yRrd8TnyI9rTaWU8fWu9rKRppzqbM9nWkB3gXhDK/SysMzC14raYxTfcaqpND2PIuCgYqGVLwGi1mDYx2rjl5rqVxxkMXEey0JsgQGJU+1U09HtxLzYTH8SpeQE=', '171.104.223.179', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/87.0.4280.88 Safari/537.36', '2020-12-18 16:20:10', '1');
INSERT INTO `db_token` VALUES ('2655', '1', 'admin', 'DSpazZTPAw74/cO3CGcMWCQXt+sVyov5PHBfsOl5DnnPbWvgK7aaG5dj07/2+f7GN2L7EVhH01Y42537BgYHv081kzMrjbEzrNM1iLIEfH2thY9m4AbI25oKmBZcmZdECdk+sJfrMQYpnheXJT21gy9fMKn1cXnHE4+Q7jHnQOGyUT2vPw4zmCFrkcqbTgQrg5uzBb1iPTXRdptaYibdOQwbZKti53hx0mFosS62DHw=', '::1', 'Mozilla/5.0 (Linux; Android 6.0; Nexus 5 Build/MRA58N) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/76.0.3809.132 Mobile Safari/537.36', '2020-12-19 11:11:45', '1');
INSERT INTO `db_token` VALUES ('2657', '1', 'admin', 'DSpazZTPAw74/cO3CGcMWCQXt+sVyov5PHBfsOl5DnnPbWvgK7aaG5dj07/2+f7GjMrHHUqbk80ynbLjDR3mMZNg/Po0tpNtWYQgOpQQXE7MN9C8XSCNpHzmmjgmsVK6D1w8OJ5gIi9eaLx1ouXHVKgmaOkn8y7g81SAxWp9IHLrS1l9hG+JGtpPDCh7lxJE33NMpTfZfq5gj6wtSe84kg==', '::1', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/76.0.3809.132 Safari/537.36', '2020-12-19 11:57:39', '1');
INSERT INTO `db_token` VALUES ('2662', '1', 'admin', 'DSpazZTPAw74/cO3CGcMWI1XSrMoX/mbSkVxRLVy/nBKFJhGpH1Rv2w6VFtprpJ9CnZ87m0DlLkHxDDBUQ5zQDGC4QunO76cObJm4MewneMCtTJ3yRrd8TnyI9rTaWU8fWu9rKRppzqbM9nWkB3gXhDK/SysMzC14raYxTfcaqpkURW7lnKB1RrEE01rAjk+3yWNTPXRvkx5WGLgLdQtp5tGI1B0hxOKO5cdkekZcew=', '171.104.223.179', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/76.0.3809.132 Safari/537.36', '2020-12-19 12:04:53', '1');
INSERT INTO `db_token` VALUES ('2665', '1', 'admin', 'DSpazZTPAw74/cO3CGcMWCQXt+sVyov5PHBfsOl5DnnPbWvgK7aaG5dj07/2+f7GjMrHHUqbk80ynbLjDR3mMZNg/Po0tpNtWYQgOpQQXE7MN9C8XSCNpHzmmjgmsVK6D1w8OJ5gIi9eaLx1ouXHVKgmaOkn8y7g81SAxWp9IHLrS1l9hG+JGtpPDCh7lxJE33NMpTfZfq5gj6wtSe84kg==', '::1', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/76.0.3809.132 Safari/537.36', '2020-12-19 15:39:03', '1');
INSERT INTO `db_token` VALUES ('2671', '1', 'admin', 'DSpazZTPAw74/cO3CGcMWI1XSrMoX/mbSkVxRLVy/nBKFJhGpH1Rv2w6VFtprpJ9CnZ87m0DlLkHxDDBUQ5zQDGC4QunO76cObJm4MewneMCtTJ3yRrd8TnyI9rTaWU8fWu9rKRppzqbM9nWkB3gXhDK/SysMzC14raYxTfcaqpkURW7lnKB1RrEE01rAjk+3yWNTPXRvkx5WGLgLdQtp5tGI1B0hxOKO5cdkekZcew=', '171.104.223.179', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/76.0.3809.132 Safari/537.36', '2020-12-19 15:50:32', '1');
INSERT INTO `db_token` VALUES ('2688', '1', 'admin', 'DSpazZTPAw74/cO3CGcMWCQXt+sVyov5PHBfsOl5DnnPbWvgK7aaG5dj07/2+f7GjMrHHUqbk80ynbLjDR3mMZNg/Po0tpNtWYQgOpQQXE7MN9C8XSCNpHzmmjgmsVK6D1w8OJ5gIi9eaLx1ouXHVFGLHDx7kQHGeN+QGF8EDq+pTEJX/LTHXEDCXiELyhqgTTnuncahsJxocj8E8LdZOjpmVtpO7/WtfpGalizgUaI=', '::1', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/87.0.4280.88 Safari/537.36 Edg/87.0.664.60', '2020-12-19 18:01:42', '1');
INSERT INTO `db_token` VALUES ('2695', '1', 'admin', 'DSpazZTPAw74/cO3CGcMWCQXt+sVyov5PHBfsOl5DnnPbWvgK7aaG5dj07/2+f7GjMrHHUqbk80ynbLjDR3mMZNg/Po0tpNtWYQgOpQQXE7MN9C8XSCNpHzmmjgmsVK6D1w8OJ5gIi9eaLx1ouXHVKgmaOkn8y7g81SAxWp9IHLrS1l9hG+JGtpPDCh7lxJE33NMpTfZfq5gj6wtSe84kg==', '::1', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/76.0.3809.132 Safari/537.36', '2020-12-20 09:31:25', '1');
INSERT INTO `db_token` VALUES ('2696', '1', 'admin', 'DSpazZTPAw74/cO3CGcMWCQXt+sVyov5PHBfsOl5DnnPbWvgK7aaG5dj07/2+f7GjMrHHUqbk80ynbLjDR3mMZNg/Po0tpNtWYQgOpQQXE7MN9C8XSCNpHzmmjgmsVK6D1w8OJ5gIi9eaLx1ouXHVKgmaOkn8y7g81SAxWp9IHLrS1l9hG+JGtpPDCh7lxJE33NMpTfZfq5gj6wtSe84kg==', '::1', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/76.0.3809.132 Safari/537.36', '2020-12-20 09:32:15', '1');
INSERT INTO `db_token` VALUES ('2702', '1', 'admin', 'DSpazZTPAw74/cO3CGcMWCQXt+sVyov5PHBfsOl5DnnPbWvgK7aaG5dj07/2+f7GN2L7EVhH01Y42537BgYHv081kzMrjbEzrNM1iLIEfH2thY9m4AbI25oKmBZcmZdECdk+sJfrMQYpnheXJT21gy9fMKn1cXnHE4+Q7jHnQOGyUT2vPw4zmCFrkcqbTgQrg5uzBb1iPTXRdptaYibdOQwbZKti53hx0mFosS62DHw=', '::1', 'Mozilla/5.0 (Linux; Android 6.0; Nexus 5 Build/MRA58N) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/76.0.3809.132 Mobile Safari/537.36', '2020-12-20 14:13:51', '1');
INSERT INTO `db_token` VALUES ('2704', '1', 'admin', 'DSpazZTPAw74/cO3CGcMWCQXt+sVyov5PHBfsOl5DnnPbWvgK7aaG5dj07/2+f7GjMrHHUqbk80ynbLjDR3mMZNg/Po0tpNtWYQgOpQQXE7MN9C8XSCNpHzmmjgmsVK6D1w8OJ5gIi9eaLx1ouXHVKgmaOkn8y7g81SAxWp9IHLrS1l9hG+JGtpPDCh7lxJE33NMpTfZfq5gj6wtSe84kg==', '::1', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/76.0.3809.132 Safari/537.36', '2020-12-20 15:20:23', '1');
INSERT INTO `db_token` VALUES ('2705', '1', 'admin', 'DSpazZTPAw74/cO3CGcMWCQXt+sVyov5PHBfsOl5DnnPbWvgK7aaG5dj07/2+f7GN2L7EVhH01Y42537BgYHv081kzMrjbEzrNM1iLIEfH2thY9m4AbI25oKmBZcmZdECdk+sJfrMQYpnheXJT21gy9fMKn1cXnHE4+Q7jHnQOGyUT2vPw4zmCFrkcqbTgQrg5uzBb1iPTXRdptaYibdOQwbZKti53hx0mFosS62DHw=', '::1', 'Mozilla/5.0 (Linux; Android 6.0; Nexus 5 Build/MRA58N) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/76.0.3809.132 Mobile Safari/537.36', '2020-12-20 15:24:18', '1');
INSERT INTO `db_token` VALUES ('2706', '1', 'admin', 'DSpazZTPAw74/cO3CGcMWEdWe9A+8RH5mzWA5Y7rrJ5HIf+jUGUD4rqzY2fuj09k03kT/yFgkUpuPNHcS2Inc5sUnref3+IIv8owkYRHJPRzWexxj8PkvPfGLs+eClav4E4WcQYwkuox4QqK+4NCLjEUPj9VErRBb6+EbhHIvXTqgyYiK0Mt/O44DPZ7jAXh7C7/W7QJ7HLXbTo/e9Kc1oQJRQZqkv67A2FXHHqrPhTMPIyXGTaTvw2HerFcmqH7', '127.0.0.1', 'Mozilla/5.0 (Linux; Android 6.0; Nexus 5 Build/MRA58N) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/76.0.3809.132 Mobile Safari/537.36', '2020-12-20 15:54:13', '1');
INSERT INTO `db_token` VALUES ('2707', '1', 'admin', 'DSpazZTPAw74/cO3CGcMWCQXt+sVyov5PHBfsOl5DnnPbWvgK7aaG5dj07/2+f7GN2L7EVhH01Y42537BgYHv081kzMrjbEzrNM1iLIEfH2thY9m4AbI25oKmBZcmZdECdk+sJfrMQYpnheXJT21gy9fMKn1cXnHE4+Q7jHnQOGyUT2vPw4zmCFrkcqbTgQrg5uzBb1iPTXRdptaYibdOQwbZKti53hx0mFosS62DHw=', '::1', 'Mozilla/5.0 (Linux; Android 6.0; Nexus 5 Build/MRA58N) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/76.0.3809.132 Mobile Safari/537.36', '2020-12-20 15:58:47', '1');
INSERT INTO `db_token` VALUES ('2717', '1', 'admin', 'DSpazZTPAw74/cO3CGcMWCQXt+sVyov5PHBfsOl5DnnPbWvgK7aaG5dj07/2+f7GN2L7EVhH01Y42537BgYHv081kzMrjbEzrNM1iLIEfH2thY9m4AbI25oKmBZcmZdECdk+sJfrMQYpnheXJT21gy9fMKn1cXnHE4+Q7jHnQOGyUT2vPw4zmCFrkcqbTgQrg5uzBb1iPTXRdptaYibdOQwbZKti53hx0mFosS62DHw=', '::1', 'Mozilla/5.0 (Linux; Android 6.0; Nexus 5 Build/MRA58N) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/76.0.3809.132 Mobile Safari/537.36', '2020-12-21 10:58:05', '1');
INSERT INTO `db_token` VALUES ('2719', '1', 'admin', 'DSpazZTPAw74/cO3CGcMWLB2EB/3rQ5xzMwD6Wh7RCPYZVndTe2FLjUap/96zfkfCnZ87m0DlLkHxDDBUQ5zQDGC4QunO76cObJm4MewneMCtTJ3yRrd8TnyI9rTaWU8fWu9rKRppzqbM9nWkB3gXhDK/SysMzC14raYxTfcaqpkURW7lnKB1RrEE01rAjk+3yWNTPXRvkx5WGLgLdQtp5tGI1B0hxOKO5cdkekZcew=', '116.252.134.151', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/76.0.3809.132 Safari/537.36', '2020-12-21 14:10:51', '1');
INSERT INTO `db_token` VALUES ('2739', '1', 'admin', 'DSpazZTPAw74/cO3CGcMWOe6s2TFv/uP93EC8N0msfXVZa79nJI4ohv57qO08a1heVJfFIctEc5WpPadGkFHQUmHw3CPXNu5hLJL+Z02v8nenfOsILcB2GNNcNBW5tgrZfsnryf3sHFxFbwP1fjC/thGvwBW0IoAR09HmnwpUYx4RZtRpMCXDw01Rsuency1SYjcqLCcziYYfvFc7y7Xncw8jJcZNpO/DYd6sVyaofs=', '222.216.43.250', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/87.0.4280.88 Safari/537.36', '2020-12-27 14:09:31', '1');
INSERT INTO `db_token` VALUES ('2749', '1', 'admin', 'DSpazZTPAw74/cO3CGcMWCQXt+sVyov5PHBfsOl5DnnPbWvgK7aaG5dj07/2+f7GjMrHHUqbk80ynbLjDR3mMZNg/Po0tpNtWYQgOpQQXE7MN9C8XSCNpHzmmjgmsVK6D1w8OJ5gIi9eaLx1ouXHVFGLHDx7kQHGeN+QGF8EDq+pTEJX/LTHXEDCXiELyhqgctCwJbv+y0AZOx+VMLC+VQ==', '::1', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/87.0.4280.88 Safari/537.36', '2021-01-10 10:10:28', '1');
INSERT INTO `db_token` VALUES ('2753', '1', 'admin', 'DSpazZTPAw74/cO3CGcMWCQXt+sVyov5PHBfsOl5DnnPbWvgK7aaG5dj07/2+f7GN2L7EVhH01Y42537BgYHv081kzMrjbEzrNM1iLIEfH2thY9m4AbI25oKmBZcmZdECdk+sJfrMQYpnheXJT21gy9fMKn1cXnHE4+Q7jHnQOGyUT2vPw4zmCFrkcqbTgQrg5uzBb1iPTXRdptaYibdOQwbZKti53hx0mFosS62DHw=', '::1', 'Mozilla/5.0 (Linux; Android 6.0; Nexus 5 Build/MRA58N) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/76.0.3809.132 Mobile Safari/537.36', '2021-01-11 14:05:58', '1');
INSERT INTO `db_token` VALUES ('2755', '1', 'admin', 'DSpazZTPAw74/cO3CGcMWCQXt+sVyov5PHBfsOl5DnnPbWvgK7aaG5dj07/2+f7GN2L7EVhH01Y42537BgYHv081kzMrjbEzrNM1iLIEfH2thY9m4AbI25oKmBZcmZdECdk+sJfrMQYpnheXJT21gy9fMKn1cXnHE4+Q7jHnQOGyUT2vPw4zmCFrkcqbTgQrg5uzBb1iPTXRdptaYibdOQwbZKti53hx0mFosS62DHw=', '::1', 'Mozilla/5.0 (Linux; Android 6.0; Nexus 5 Build/MRA58N) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/76.0.3809.132 Mobile Safari/537.36', '2021-01-11 14:58:08', '1');
INSERT INTO `db_token` VALUES ('2756', '1', 'admin', 'DSpazZTPAw74/cO3CGcMWCQXt+sVyov5PHBfsOl5DnnPbWvgK7aaG5dj07/2+f7GjMrHHUqbk80ynbLjDR3mMZNg/Po0tpNtWYQgOpQQXE7MN9C8XSCNpHzmmjgmsVK6D1w8OJ5gIi9eaLx1ouXHVKgmaOkn8y7g81SAxWp9IHLrS1l9hG+JGtpPDCh7lxJE33NMpTfZfq5gj6wtSe84kg==', '::1', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/76.0.3809.132 Safari/537.36', '2021-01-11 16:50:51', '1');
INSERT INTO `db_token` VALUES ('2757', '1', 'admin', 'DSpazZTPAw74/cO3CGcMWCQXt+sVyov5PHBfsOl5DnnPbWvgK7aaG5dj07/2+f7GN2L7EVhH01Y42537BgYHv081kzMrjbEzrNM1iLIEfH2thY9m4AbI25oKmBZcmZdECdk+sJfrMQYpnheXJT21gy9fMKn1cXnHE4+Q7jHnQOGyUT2vPw4zmCFrkcqbTgQrg5uzBb1iPTXRdptaYibdOQwbZKti53hx0mFosS62DHw=', '::1', 'Mozilla/5.0 (Linux; Android 6.0; Nexus 5 Build/MRA58N) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/76.0.3809.132 Mobile Safari/537.36', '2021-01-11 17:26:21', '1');
INSERT INTO `db_token` VALUES ('2758', '1', 'admin', 'DSpazZTPAw74/cO3CGcMWCQXt+sVyov5PHBfsOl5DnnPbWvgK7aaG5dj07/2+f7GjMrHHUqbk80ynbLjDR3mMZNg/Po0tpNtWYQgOpQQXE7MN9C8XSCNpHzmmjgmsVK6D1w8OJ5gIi9eaLx1ouXHVKgmaOkn8y7g81SAxWp9IHLrS1l9hG+JGtpPDCh7lxJE33NMpTfZfq5gj6wtSe84kg==', '::1', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/76.0.3809.132 Safari/537.36', '2021-01-11 17:27:10', '1');
INSERT INTO `db_token` VALUES ('2759', '1', 'admin', 'DSpazZTPAw74/cO3CGcMWCQXt+sVyov5PHBfsOl5DnnPbWvgK7aaG5dj07/2+f7GN2L7EVhH01Y42537BgYHv081kzMrjbEzrNM1iLIEfH2thY9m4AbI25oKmBZcmZdECdk+sJfrMQYpnheXJT21gy9fMKn1cXnHE4+Q7jHnQOGyUT2vPw4zmCFrkcqbTgQrg5uzBb1iPTXRdptaYibdOQwbZKti53hx0mFosS62DHw=', '::1', 'Mozilla/5.0 (Linux; Android 6.0; Nexus 5 Build/MRA58N) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/76.0.3809.132 Mobile Safari/537.36', '2021-01-11 17:49:39', '1');
INSERT INTO `db_token` VALUES ('2760', '1', 'admin', 'DSpazZTPAw74/cO3CGcMWCQXt+sVyov5PHBfsOl5DnnPbWvgK7aaG5dj07/2+f7G66oCS1i2y0jJDLfyAtKwow2brV2mi1qe0Lzz/v6wAx6YasL/eKC4++pDNTAauXJUGLhd3kbZCOcB1ZGr6gpxH2RExIlFUVbOV7MqaBE9ZwsFrArEAUjNwkjbyaa62A3H9AubOcLw2fffx1OxmlJEgoW946s2dTGUEw0qvH07NtE=', '::1', 'Mozilla/5.0 (iPad; CPU OS 11_0 like Mac OS X) AppleWebKit/604.1.34 (KHTML, like Gecko) Version/11.0 Mobile/15A5341f Safari/604.1', '2021-01-11 17:50:03', '1');
INSERT INTO `db_token` VALUES ('2761', '1', 'admin', 'DSpazZTPAw74/cO3CGcMWCQXt+sVyov5PHBfsOl5DnnPbWvgK7aaG5dj07/2+f7GjMrHHUqbk80ynbLjDR3mMZNg/Po0tpNtWYQgOpQQXE7MN9C8XSCNpHzmmjgmsVK6D1w8OJ5gIi9eaLx1ouXHVKgmaOkn8y7g81SAxWp9IHLrS1l9hG+JGtpPDCh7lxJE33NMpTfZfq5gj6wtSe84kg==', '::1', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/76.0.3809.132 Safari/537.36', '2021-01-11 17:51:52', '1');
INSERT INTO `db_token` VALUES ('2762', '1', 'admin', 'DSpazZTPAw74/cO3CGcMWCQXt+sVyov5PHBfsOl5DnnPbWvgK7aaG5dj07/2+f7GN2L7EVhH01Y42537BgYHv081kzMrjbEzrNM1iLIEfH2thY9m4AbI25oKmBZcmZdECdk+sJfrMQYpnheXJT21gy9fMKn1cXnHE4+Q7jHnQOGyUT2vPw4zmCFrkcqbTgQrg5uzBb1iPTXRdptaYibdOQwbZKti53hx0mFosS62DHw=', '::1', 'Mozilla/5.0 (Linux; Android 6.0; Nexus 5 Build/MRA58N) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/76.0.3809.132 Mobile Safari/537.36', '2021-01-15 10:04:34', '1');
INSERT INTO `db_token` VALUES ('2763', '1', 'admin', 'DSpazZTPAw74/cO3CGcMWCQXt+sVyov5PHBfsOl5DnnPbWvgK7aaG5dj07/2+f7GN2L7EVhH01Y42537BgYHv081kzMrjbEzrNM1iLIEfH2thY9m4AbI25oKmBZcmZdECdk+sJfrMQYpnheXJT21gy9fMKn1cXnHE4+Q7jHnQOGyUT2vPw4zmCFrkcqbTgQrg5uzBb1iPTXRdptaYibdOQwbZKti53hx0mFosS62DHw=', '::1', 'Mozilla/5.0 (Linux; Android 6.0; Nexus 5 Build/MRA58N) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/76.0.3809.132 Mobile Safari/537.36', '2021-01-15 10:07:43', '1');
INSERT INTO `db_token` VALUES ('2766', '1', 'admin', 'DSpazZTPAw74/cO3CGcMWLB2EB/3rQ5xzMwD6Wh7RCNU+ZL9kA/hNPFRZfuX9sLNajLfPu7J48lIiBVT/33qCG74BGKZu0O2e1xTlHxdtqdP+m9MBe0dlJcEgkQvxKQTaYfl+PeQIrAXNjFIKw7VWhGtIU6CUf/hxfngO9ateyFHfJXWjGEPA2fpHwVmP+5Zg2FKWmKpyCjNwc8CncskVsw8jJcZNpO/DYd6sVyaofs=', '116.252.95.78', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/87.0.4280.141 Safari/537.36', '2021-01-21 15:17:53', '1');
INSERT INTO `db_token` VALUES ('2767', '1', 'admin', 'DSpazZTPAw74/cO3CGcMWLB2EB/3rQ5xzMwD6Wh7RCNU+ZL9kA/hNPFRZfuX9sLNajLfPu7J48lIiBVT/33qCG74BGKZu0O2e1xTlHxdtqdP+m9MBe0dlJcEgkQvxKQTaYfl+PeQIrAXNjFIKw7VWhGtIU6CUf/hxfngO9ateyFHfJXWjGEPA2fpHwVmP+5Zg2FKWmKpyCjNwc8CncskVsw8jJcZNpO/DYd6sVyaofs=', '116.252.95.78', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/87.0.4280.141 Safari/537.36', '2021-01-22 09:32:42', '1');
INSERT INTO `db_token` VALUES ('2769', '1', 'admin', 'DSpazZTPAw74/cO3CGcMWLsNNCE8qPrn+uUg4cydRnWESpaghPYLq5oMfVKUtdLqajLfPu7J48lIiBVT/33qCG74BGKZu0O2e1xTlHxdtqdP+m9MBe0dlJcEgkQvxKQTaYfl+PeQIrAXNjFIKw7VWhGtIU6CUf/hxfngO9ateyFHfJXWjGEPA2fpHwVmP+5Zg2FKWmKpyCjNwc8CncskVsw8jJcZNpO/DYd6sVyaofs=', '171.107.9.113', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/87.0.4280.141 Safari/537.36', '2021-01-23 22:02:05', '1');
INSERT INTO `db_token` VALUES ('2770', '1', 'admin', 'DSpazZTPAw74/cO3CGcMWCQXt+sVyov5PHBfsOl5DnnPbWvgK7aaG5dj07/2+f7GN2L7EVhH01Y42537BgYHv081kzMrjbEzrNM1iLIEfH2thY9m4AbI25oKmBZcmZdECdk+sJfrMQYpnheXJT21gy9fMKn1cXnHE4+Q7jHnQOGyUT2vPw4zmCFrkcqbTgQrz0u59S+E4WalCyTrjrpqnAwbZKti53hx0mFosS62DHw=', '::1', 'Mozilla/5.0 (Linux; Android 6.0; Nexus 5 Build/MRA58N) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/76.0.3809.100 Mobile Safari/537.36', '2021-01-27 10:50:18', '1');
INSERT INTO `db_token` VALUES ('2772', '1', 'admin', 'DSpazZTPAw74/cO3CGcMWCQXt+sVyov5PHBfsOl5DnnPbWvgK7aaG5dj07/2+f7GN2L7EVhH01Y42537BgYHv081kzMrjbEzrNM1iLIEfH2thY9m4AbI25oKmBZcmZdECdk+sJfrMQYpnheXJT21gy9fMKn1cXnHE4+Q7jHnQOGyUT2vPw4zmCFrkcqbTgQrz0u59S+E4WalCyTrjrpqnAwbZKti53hx0mFosS62DHw=', '::1', 'Mozilla/5.0 (Linux; Android 6.0; Nexus 5 Build/MRA58N) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/76.0.3809.100 Mobile Safari/537.36', '2021-01-27 11:06:24', '1');
INSERT INTO `db_token` VALUES ('2774', '1', 'admin', 'DSpazZTPAw74/cO3CGcMWCQXt+sVyov5PHBfsOl5DnnPbWvgK7aaG5dj07/2+f7GN2L7EVhH01Y42537BgYHv081kzMrjbEzrNM1iLIEfH2thY9m4AbI25oKmBZcmZdECdk+sJfrMQYpnheXJT21gy9fMKn1cXnHE4+Q7jHnQOGyUT2vPw4zmCFrkcqbTgQrz0u59S+E4WalCyTrjrpqnAwbZKti53hx0mFosS62DHw=', '::1', 'Mozilla/5.0 (Linux; Android 6.0; Nexus 5 Build/MRA58N) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/76.0.3809.100 Mobile Safari/537.36', '2021-01-27 11:30:35', '1');
INSERT INTO `db_token` VALUES ('2776', '1', 'admin', 'DSpazZTPAw74/cO3CGcMWCQXt+sVyov5PHBfsOl5DnnPbWvgK7aaG5dj07/2+f7GN2L7EVhH01Y42537BgYHv081kzMrjbEzrNM1iLIEfH2thY9m4AbI25oKmBZcmZdECdk+sJfrMQYpnheXJT21gy9fMKn1cXnHE4+Q7jHnQOGyUT2vPw4zmCFrkcqbTgQrz0u59S+E4WalCyTrjrpqnAwbZKti53hx0mFosS62DHw=', '::1', 'Mozilla/5.0 (Linux; Android 6.0; Nexus 5 Build/MRA58N) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/76.0.3809.100 Mobile Safari/537.36', '2021-01-27 11:49:12', '1');
INSERT INTO `db_token` VALUES ('2778', '1', 'admin', 'DSpazZTPAw74/cO3CGcMWCQXt+sVyov5PHBfsOl5DnnPbWvgK7aaG5dj07/2+f7GN2L7EVhH01Y42537BgYHv081kzMrjbEzrNM1iLIEfH2thY9m4AbI25oKmBZcmZdECdk+sJfrMQYpnheXJT21gy9fMKn1cXnHE4+Q7jHnQOGyUT2vPw4zmCFrkcqbTgQrz0u59S+E4WalCyTrjrpqnAwbZKti53hx0mFosS62DHw=', '::1', 'Mozilla/5.0 (Linux; Android 6.0; Nexus 5 Build/MRA58N) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/76.0.3809.100 Mobile Safari/537.36', '2021-01-27 12:09:03', '1');
INSERT INTO `db_token` VALUES ('2781', '1', 'admin', 'DSpazZTPAw74/cO3CGcMWCQXt+sVyov5PHBfsOl5DnnPbWvgK7aaG5dj07/2+f7GjMrHHUqbk80ynbLjDR3mMSeZgJUo7xz5z5OR7jm2WXdl+yevJ/ewcXEVvA/V+ML+2Ea/AFbQigBHT0eafClRjKBJtb2y/Msantrj+QUDkVxl+pM5qndmGKE7gLSI7286YlT7VTT0e3EvNhMfxKl5AQ==', '::1', 'Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/76.0.3809.100 Safari/537.36', '2021-01-27 12:24:01', '1');
INSERT INTO `db_token` VALUES ('2783', '1', 'admin', 'DSpazZTPAw74/cO3CGcMWCQXt+sVyov5PHBfsOl5DnnPbWvgK7aaG5dj07/2+f7GjMrHHUqbk80ynbLjDR3mMSeZgJUo7xz5z5OR7jm2WXdl+yevJ/ewcXEVvA/V+ML+2Ea/AFbQigBHT0eafClRjKBJtb2y/Msantrj+QUDkVxl+pM5qndmGKE7gLSI7286YlT7VTT0e3EvNhMfxKl5AQ==', '::1', 'Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/76.0.3809.100 Safari/537.36', '2021-01-27 12:30:21', '1');
INSERT INTO `db_token` VALUES ('2786', '1', 'admin', 'DSpazZTPAw74/cO3CGcMWCQXt+sVyov5PHBfsOl5DnnPbWvgK7aaG5dj07/2+f7GjMrHHUqbk80ynbLjDR3mMZNg/Po0tpNtWYQgOpQQXE7MN9C8XSCNpHzmmjgmsVK6D1w8OJ5gIi9eaLx1ouXHVFGLHDx7kQHGeN+QGF8EDq8Tij9QUGHL6FcbygBtfKI/33NMpTfZfq5gj6wtSe84kg==', '::1', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/87.0.4280.141 Safari/537.36', '2021-01-29 18:45:40', '1');
INSERT INTO `db_token` VALUES ('2798', '1', 'admin', 'DSpazZTPAw74/cO3CGcMWCQXt+sVyov5PHBfsOl5DnnPbWvgK7aaG5dj07/2+f7GjMrHHUqbk80ynbLjDR3mMZNg/Po0tpNtWYQgOpQQXE7MN9C8XSCNpHzmmjgmsVK6D1w8OJ5gIi9eaLx1ouXHVFGLHDx7kQHGeN+QGF8EDq8Tij9QUGHL6FcbygBtfKI/33NMpTfZfq5gj6wtSe84kg==', '::1', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/87.0.4280.141 Safari/537.36', '2021-02-01 11:22:46', '1');
INSERT INTO `db_token` VALUES ('2799', '1', 'admin', 'DSpazZTPAw74/cO3CGcMWEdWe9A+8RH5mzWA5Y7rrJ5HIf+jUGUD4rqzY2fuj09kemwaUcOPrV191eyaA7oM0qSPDcGZQLUgKQvtJ6wBjtDwp35CUvVAJSgudiVkBSs9R/HVXet49S/q5gkGIXB1I583fSEFofl+86DqNfuNsA1R1xFSPyIq8M3jxXnL1dGpMKR36kiNO19nopzTsTrwrA==', '127.0.0.1', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/87.0.4280.141 Safari/537.36', '2021-02-01 15:28:10', '1');
INSERT INTO `db_token` VALUES ('2800', '1', 'admin', 'DSpazZTPAw74/cO3CGcMWCQXt+sVyov5PHBfsOl5DnnPbWvgK7aaG5dj07/2+f7GjMrHHUqbk80ynbLjDR3mMZNg/Po0tpNtWYQgOpQQXE7MN9C8XSCNpHzmmjgmsVK6D1w8OJ5gIi9eaLx1ouXHVFGLHDx7kQHGeN+QGF8EDq8Tij9QUGHL6FcbygBtfKI/33NMpTfZfq5gj6wtSe84kg==', '::1', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/87.0.4280.141 Safari/537.36', '2021-02-01 15:29:59', '1');
INSERT INTO `db_token` VALUES ('2801', '1', 'admin', 'DSpazZTPAw74/cO3CGcMWCQXt+sVyov5PHBfsOl5DnnPbWvgK7aaG5dj07/2+f7GjMrHHUqbk80ynbLjDR3mMZNg/Po0tpNtWYQgOpQQXE7MN9C8XSCNpHzmmjgmsVK6D1w8OJ5gIi9eaLx1ouXHVOcibRVdGnIbF6zEm6bLxW70mBze2gLh/+qt4nLG9+Qi33NMpTfZfq5gj6wtSe84kg==', '::1', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/88.0.4324.104 Safari/537.36', '2021-02-04 09:33:21', '1');
INSERT INTO `db_token` VALUES ('2803', '1', 'admin', 'DSpazZTPAw74/cO3CGcMWCQXt+sVyov5PHBfsOl5DnnPbWvgK7aaG5dj07/2+f7GjMrHHUqbk80ynbLjDR3mMZNg/Po0tpNtWYQgOpQQXE7MN9C8XSCNpHzmmjgmsVK6D1w8OJ5gIi9eaLx1ouXHVOcibRVdGnIbF6zEm6bLxW70mBze2gLh/+qt4nLG9+Qi33NMpTfZfq5gj6wtSe84kg==', '::1', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/88.0.4324.104 Safari/537.36', '2021-02-04 15:19:22', '1');
INSERT INTO `db_token` VALUES ('2804', '1', 'admin', 'DSpazZTPAw74/cO3CGcMWEdWe9A+8RH5mzWA5Y7rrJ5HIf+jUGUD4rqzY2fuj09kemwaUcOPrV191eyaA7oM0qSPDcGZQLUgKQvtJ6wBjtDwp35CUvVAJSgudiVkBSs9R/HVXet49S/q5gkGIXB1I583fSEFofl+86DqNfuNsA3LNnriN7ziD2sVygRLV815MKR36kiNO19nopzTsTrwrA==', '127.0.0.1', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/88.0.4324.104 Safari/537.36', '2021-02-04 16:30:39', '1');
INSERT INTO `db_token` VALUES ('2805', '1', 'admin', 'DSpazZTPAw74/cO3CGcMWCQXt+sVyov5PHBfsOl5DnnPbWvgK7aaG5dj07/2+f7GjMrHHUqbk80ynbLjDR3mMZNg/Po0tpNtWYQgOpQQXE7MN9C8XSCNpHzmmjgmsVK6D1w8OJ5gIi9eaLx1ouXHVOcibRVdGnIbF6zEm6bLxW70mBze2gLh/+qt4nLG9+Qi33NMpTfZfq5gj6wtSe84kg==', '::1', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/88.0.4324.104 Safari/537.36', '2021-02-04 16:36:22', '1');
INSERT INTO `db_token` VALUES ('2806', '1', 'admin', 'DSpazZTPAw74/cO3CGcMWCQXt+sVyov5PHBfsOl5DnnPbWvgK7aaG5dj07/2+f7GjMrHHUqbk80ynbLjDR3mMZNg/Po0tpNtWYQgOpQQXE7MN9C8XSCNpHzmmjgmsVK6D1w8OJ5gIi9eaLx1ouXHVOcibRVdGnIbF6zEm6bLxW5/qSTEmy0Vgaprf1fRfLo7n7eppqfDN7994az2Ensf6h00fGKbDjeBUDHyq/DL3a0=', '::1', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/88.0.4324.96 Safari/537.36 Edg/88.0.705.56', '2021-02-05 14:55:16', '1');
INSERT INTO `db_token` VALUES ('2807', '1', 'admin', 'DSpazZTPAw74/cO3CGcMWEN7Ad3Q29lW/hUapKcWs4yDe4y1S+35NO9dZYx8NpKneVJfFIctEc5WpPadGkFHQUmHw3CPXNu5hLJL+Z02v8kMOHh7Eowuc2OgB77i7IP5CuDTMI4DG8+YoREfXUbAKGN9AwC+0pxfXZljWPADNXM=', '114.250.86.106', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:85.0) Gecko/20100101 Firefox/85.0', '2021-02-07 16:03:58', '1');
INSERT INTO `db_token` VALUES ('2815', '1', 'admin', 'DSpazZTPAw74/cO3CGcMWCQXt+sVyov5PHBfsOl5DnnPbWvgK7aaG5dj07/2+f7GN2L7EVhH01Y42537BgYHv081kzMrjbEzrNM1iLIEfH2thY9m4AbI25oKmBZcmZdECdk+sJfrMQYpnheXJT21gy9fMKn1cXnHE4+Q7jHnQOGyUT2vPw4zmCFrkcqbTgQrg5uzBb1iPTXRdptaYibdOQwbZKti53hx0mFosS62DHw=', '::1', 'Mozilla/5.0 (Linux; Android 6.0; Nexus 5 Build/MRA58N) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/76.0.3809.132 Mobile Safari/537.36', '2021-02-08 16:29:06', '1');
INSERT INTO `db_token` VALUES ('2816', '1', 'admin', 'DSpazZTPAw74/cO3CGcMWCQXt+sVyov5PHBfsOl5DnnPbWvgK7aaG5dj07/2+f7GjMrHHUqbk80ynbLjDR3mMZNg/Po0tpNtWYQgOpQQXE7MN9C8XSCNpHzmmjgmsVK6D1w8OJ5gIi9eaLx1ouXHVKgmaOkn8y7g81SAxWp9IHLrS1l9hG+JGtpPDCh7lxJE33NMpTfZfq5gj6wtSe84kg==', '::1', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/76.0.3809.132 Safari/537.36', '2021-02-08 16:39:12', '1');
INSERT INTO `db_token` VALUES ('2817', '1', 'admin', 'DSpazZTPAw74/cO3CGcMWCQXt+sVyov5PHBfsOl5DnnPbWvgK7aaG5dj07/2+f7GN2L7EVhH01Y42537BgYHv081kzMrjbEzrNM1iLIEfH2thY9m4AbI25oKmBZcmZdECdk+sJfrMQYpnheXJT21gy9fMKn1cXnHE4+Q7jHnQOGyUT2vPw4zmCFrkcqbTgQrg5uzBb1iPTXRdptaYibdOQwbZKti53hx0mFosS62DHw=', '::1', 'Mozilla/5.0 (Linux; Android 6.0; Nexus 5 Build/MRA58N) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/76.0.3809.132 Mobile Safari/537.36', '2021-02-08 16:39:39', '1');
INSERT INTO `db_token` VALUES ('2818', '1', 'admin', 'DSpazZTPAw74/cO3CGcMWIcYpNe3/lX0kY4XnR9OjQ+pErmcFz9OYG3N/XgQ8nCIBxYmDd7rfz+kIvdLWeucGGh2Uzu9HVoobblGsvUkuqvzTd5jMtbIe1vdhiGFklNjLRqI43cN7fX67gLgTh9ANTv6iugU5xRkvYPLZnPlZhkArSD7iSVy+GmFSRebSbU+bb3eXO0FqdjIyI28+qltyFeNNXyd7MzaARypzHTtxqk=', '180.141.66.6', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/88.0.4324.146 Safari/537.36', '2021-02-08 16:41:40', '1');
INSERT INTO `db_token` VALUES ('2820', '1', 'admin', 'DSpazZTPAw74/cO3CGcMWCQXt+sVyov5PHBfsOl5DnnPbWvgK7aaG5dj07/2+f7GN2L7EVhH01Y42537BgYHv081kzMrjbEzrNM1iLIEfH2thY9m4AbI25oKmBZcmZdECdk+sJfrMQYpnheXJT21gy9fMKn1cXnHE4+Q7jHnQOGyUT2vPw4zmCFrkcqbTgQrg5uzBb1iPTXRdptaYibdOQwbZKti53hx0mFosS62DHw=', '::1', 'Mozilla/5.0 (Linux; Android 6.0; Nexus 5 Build/MRA58N) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/76.0.3809.132 Mobile Safari/537.36', '2021-02-08 18:13:01', '1');
INSERT INTO `db_token` VALUES ('2821', '1', 'admin', 'DSpazZTPAw74/cO3CGcMWCQXt+sVyov5PHBfsOl5DnnPbWvgK7aaG5dj07/2+f7GN2L7EVhH01Y42537BgYHv081kzMrjbEzrNM1iLIEfH2thY9m4AbI25oKmBZcmZdECdk+sJfrMQYpnheXJT21gy9fMKn1cXnHE4+Q7jHnQOGyUT2vPw4zmCFrkcqbTgQrg5uzBb1iPTXRdptaYibdOQwbZKti53hx0mFosS62DHw=', '::1', 'Mozilla/5.0 (Linux; Android 6.0; Nexus 5 Build/MRA58N) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/76.0.3809.132 Mobile Safari/537.36', '2021-02-09 10:59:01', '1');
INSERT INTO `db_token` VALUES ('2822', '1', 'admin', 'DSpazZTPAw74/cO3CGcMWCQXt+sVyov5PHBfsOl5DnnPbWvgK7aaG5dj07/2+f7GjMrHHUqbk80ynbLjDR3mMZNg/Po0tpNtWYQgOpQQXE7MN9C8XSCNpHzmmjgmsVK6D1w8OJ5gIi9eaLx1ouXHVKgmaOkn8y7g81SAxWp9IHLrS1l9hG+JGtpPDCh7lxJE33NMpTfZfq5gj6wtSe84kg==', '::1', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/76.0.3809.132 Safari/537.36', '2021-02-09 11:38:03', '1');
INSERT INTO `db_token` VALUES ('2823', '1', 'admin', 'DSpazZTPAw74/cO3CGcMWCQXt+sVyov5PHBfsOl5DnnPbWvgK7aaG5dj07/2+f7GN2L7EVhH01Y42537BgYHv081kzMrjbEzrNM1iLIEfH2thY9m4AbI25oKmBZcmZdECdk+sJfrMQYpnheXJT21gy9fMKn1cXnHE4+Q7jHnQOGyUT2vPw4zmCFrkcqbTgQrg5uzBb1iPTXRdptaYibdOQwbZKti53hx0mFosS62DHw=', '::1', 'Mozilla/5.0 (Linux; Android 6.0; Nexus 5 Build/MRA58N) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/76.0.3809.132 Mobile Safari/537.36', '2021-02-09 11:40:34', '1');
INSERT INTO `db_token` VALUES ('2824', '1', 'admin', 'DSpazZTPAw74/cO3CGcMWCQXt+sVyov5PHBfsOl5DnnPbWvgK7aaG5dj07/2+f7GN2L7EVhH01Y42537BgYHv081kzMrjbEzrNM1iLIEfH2thY9m4AbI25oKmBZcmZdECdk+sJfrMQYpnheXJT21gy9fMKn1cXnHE4+Q7jHnQOGyUT2vPw4zmCFrkcqbTgQrg5uzBb1iPTXRdptaYibdOQwbZKti53hx0mFosS62DHw=', '::1', 'Mozilla/5.0 (Linux; Android 6.0; Nexus 5 Build/MRA58N) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/76.0.3809.132 Mobile Safari/537.36', '2021-02-09 11:57:36', '1');
INSERT INTO `db_token` VALUES ('2826', '1', 'admin', 'DSpazZTPAw74/cO3CGcMWCQXt+sVyov5PHBfsOl5DnnPbWvgK7aaG5dj07/2+f7GjMrHHUqbk80ynbLjDR3mMZNg/Po0tpNtWYQgOpQQXE7MN9C8XSCNpHzmmjgmsVK6D1w8OJ5gIi9eaLx1ouXHVOcibRVdGnIbF6zEm6bLxW4yGJh97kPKsgrnwXBExVbAs1MupsbYYoFy3Yi7tvBBKDwEphQHgN8qdWkxpA++Hic=', '::1', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/88.0.4324.150 Safari/537.36 Edg/88.0.705.63', '2021-02-09 13:42:56', '1');
INSERT INTO `db_token` VALUES ('2834', '1', 'admin', 'DSpazZTPAw74/cO3CGcMWIcYpNe3/lX0kY4XnR9OjQ+pErmcFz9OYG3N/XgQ8nCIvSfYWuJBPlOOSVG2Qho9O5Bb9+QY5WCNjuz9wd5H3e+uN8DBSIlTtzp+yUb3cVYoW7HDL770eaNmpDurizFz60fx1V3rePUv6uYJBiFwdSOfN30hBaH5fvOg6jX7jbANoy0Eugl8Nu4bzRq9EGRBCHb2Cl6jgaG1dIRK1EJElYiTpJc6xGiX0P4DbbQ7BWk3', '180.141.66.6', 'Mozilla/5.0 (Linux; Android 6.0; Nexus 5 Build/MRA58N) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/76.0.3809.132 Mobile Safari/537.36', '2021-02-09 18:18:03', '1');
INSERT INTO `db_token` VALUES ('2838', '1', 'admin', 'DSpazZTPAw74/cO3CGcMWCQXt+sVyov5PHBfsOl5DnnPbWvgK7aaG5dj07/2+f7Gsy4QC9fbnf3XJhDS3GLouuE6VzLc/YMx9+OD6Df44mkCGxtOTYectggi9Pzn+VRar3IvGIE0jCalDGDi7YrLm8PrNSSHXA4fmD1mfsWX+QkxESXTxwYmMRgV51XWRNYdwewajzsJXBDbaFKZ5dILVn6cGBf2BDhHfLzkL0zXBJ5lURKzqhHQCPr3iZfKRBWl', '::1', 'Mozilla/5.0 (iPhone; CPU iPhone OS 13_2_3 like Mac OS X) AppleWebKit/605.1.15 (KHTML, like Gecko) Version/13.0.3 Mobile/15E148 Safari/604.1', '2021-02-10 10:59:27', '1');
INSERT INTO `db_token` VALUES ('2839', '1', 'admin', 'DSpazZTPAw74/cO3CGcMWIcYpNe3/lX0kY4XnR9OjQ+pErmcFz9OYG3N/XgQ8nCIBxYmDd7rfz+kIvdLWeucGGh2Uzu9HVoobblGsvUkuqvzTd5jMtbIe1vdhiGFklNjLRqI43cN7fX67gLgTh9ANTv6iugU5xRkvYPLZnPlZhlJ8C1k6HswIglMIm2URRdr4OLu2iQJB2KOL8B9sckEGFeNNXyd7MzaARypzHTtxqk=', '180.141.66.6', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/76.0.3809.132 Safari/537.36', '2021-02-10 11:03:41', '1');
INSERT INTO `db_token` VALUES ('2840', '1', 'admin', 'DSpazZTPAw74/cO3CGcMWCQXt+sVyov5PHBfsOl5DnnPbWvgK7aaG5dj07/2+f7GjMrHHUqbk80ynbLjDR3mMZNg/Po0tpNtWYQgOpQQXE7MN9C8XSCNpHzmmjgmsVK6D1w8OJ5gIi9eaLx1ouXHVOcibRVdGnIbF6zEm6bLxW5n8buNjnlVzmt6rJ+Kaoq633NMpTfZfq5gj6wtSe84kg==', '::1', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/88.0.4324.146 Safari/537.36', '2021-02-10 11:29:29', '1');
INSERT INTO `db_token` VALUES ('2841', '1', 'admin', 'DSpazZTPAw74/cO3CGcMWCQXt+sVyov5PHBfsOl5DnnPbWvgK7aaG5dj07/2+f7Gsy4QC9fbnf3XJhDS3GLouuE6VzLc/YMx9+OD6Df44mkCGxtOTYectggi9Pzn+VRar3IvGIE0jCalDGDi7YrLm8PrNSSHXA4fmD1mfsWX+QkxESXTxwYmMRgV51XWRNYdwewajzsJXBDbaFKZ5dILVn6cGBf2BDhHfLzkL0zXBJ5lURKzqhHQCPr3iZfKRBWl', '::1', 'Mozilla/5.0 (iPhone; CPU iPhone OS 13_2_3 like Mac OS X) AppleWebKit/605.1.15 (KHTML, like Gecko) Version/13.0.3 Mobile/15E148 Safari/604.1', '2021-02-10 11:33:24', '1');
INSERT INTO `db_token` VALUES ('2842', '1', 'admin', 'DSpazZTPAw74/cO3CGcMWCQXt+sVyov5PHBfsOl5DnnPbWvgK7aaG5dj07/2+f7GjMrHHUqbk80ynbLjDR3mMZNg/Po0tpNtWYQgOpQQXE7MN9C8XSCNpHzmmjgmsVK6D1w8OJ5gIi9eaLx1ouXHVOcibRVdGnIbF6zEm6bLxW5n8buNjnlVzmt6rJ+Kaoq633NMpTfZfq5gj6wtSe84kg==', '::1', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/88.0.4324.146 Safari/537.36', '2021-02-10 11:37:03', '1');
INSERT INTO `db_token` VALUES ('2843', '1', 'admin', 'DSpazZTPAw74/cO3CGcMWCQXt+sVyov5PHBfsOl5DnnPbWvgK7aaG5dj07/2+f7GN2L7EVhH01Y42537BgYHv081kzMrjbEzrNM1iLIEfH2thY9m4AbI25oKmBZcmZdECdk+sJfrMQYpnheXJT21gy9fMKn1cXnHE4+Q7jHnQOGyUT2vPw4zmCFrkcqbTgQrg5uzBb1iPTXRdptaYibdOQwbZKti53hx0mFosS62DHw=', '::1', 'Mozilla/5.0 (Linux; Android 6.0; Nexus 5 Build/MRA58N) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/76.0.3809.132 Mobile Safari/537.36', '2021-02-10 11:52:00', '1');
INSERT INTO `db_token` VALUES ('2845', '1', 'admin', 'DSpazZTPAw74/cO3CGcMWCQXt+sVyov5PHBfsOl5DnnPbWvgK7aaG5dj07/2+f7Gsy4QC9fbnf3XJhDS3GLouinJ/zHUTECiOVmHQv6k6nVZoPkMTYlZ4nQKFsGQzU8aR8kcCeWNZcWrMdYdU1EKeQR9B4CQp/hZDNTZpJWfrhStuoJnLH6Wodbm7Uq0ELrdK0DOwQ1lalDI8t2+FvnGUZDHkHR+cv/AceIUhMzbpoU=', '::1', 'Mozilla/5.0 (iPhone; CPU iPhone OS 11_0 like Mac OS X) AppleWebKit/604.1.38 (KHTML, like Gecko) Version/11.0 Mobile/15A372 Safari/604.1', '2021-02-10 11:59:23', '1');
INSERT INTO `db_token` VALUES ('2846', '1', 'admin', 'DSpazZTPAw74/cO3CGcMWCQXt+sVyov5PHBfsOl5DnnPbWvgK7aaG5dj07/2+f7GjMrHHUqbk80ynbLjDR3mMZNg/Po0tpNtWYQgOpQQXE7MN9C8XSCNpHzmmjgmsVK6D1w8OJ5gIi9eaLx1ouXHVOcibRVdGnIbF6zEm6bLxW5n8buNjnlVzmt6rJ+Kaoq633NMpTfZfq5gj6wtSe84kg==', '::1', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/88.0.4324.146 Safari/537.36', '2021-02-10 12:05:18', '1');
INSERT INTO `db_token` VALUES ('2847', '1', 'admin', 'DSpazZTPAw74/cO3CGcMWCQXt+sVyov5PHBfsOl5DnnPbWvgK7aaG5dj07/2+f7GN2L7EVhH01Y42537BgYHv081kzMrjbEzrNM1iLIEfH2thY9m4AbI25oKmBZcmZdECdk+sJfrMQYpnheXJT21gy9fMKn1cXnHE4+Q7jHnQOGyUT2vPw4zmCFrkcqbTgQrg5uzBb1iPTXRdptaYibdOQwbZKti53hx0mFosS62DHw=', '::1', 'Mozilla/5.0 (Linux; Android 6.0; Nexus 5 Build/MRA58N) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/76.0.3809.132 Mobile Safari/537.36', '2021-02-10 12:07:35', '1');
INSERT INTO `db_token` VALUES ('2848', '1', 'admin', 'DSpazZTPAw74/cO3CGcMWCQXt+sVyov5PHBfsOl5DnnPbWvgK7aaG5dj07/2+f7Gsy4QC9fbnf3XJhDS3GLouuE6VzLc/YMx9+OD6Df44mkCGxtOTYectggi9Pzn+VRar3IvGIE0jCalDGDi7YrLm8PrNSSHXA4fmD1mfsWX+QkxESXTxwYmMRgV51XWRNYdwewajzsJXBDbaFKZ5dILVn6cGBf2BDhHfLzkL0zXBJ5lURKzqhHQCPr3iZfKRBWl', '::1', 'Mozilla/5.0 (iPhone; CPU iPhone OS 13_2_3 like Mac OS X) AppleWebKit/605.1.15 (KHTML, like Gecko) Version/13.0.3 Mobile/15E148 Safari/604.1', '2021-02-10 12:08:43', '1');
INSERT INTO `db_token` VALUES ('2849', '1', 'admin', 'DSpazZTPAw74/cO3CGcMWCQXt+sVyov5PHBfsOl5DnnPbWvgK7aaG5dj07/2+f7Gsy4QC9fbnf3XJhDS3GLouinJ/zHUTECiOVmHQv6k6nVZoPkMTYlZ4nQKFsGQzU8aR8kcCeWNZcWrMdYdU1EKeQR9B4CQp/hZDNTZpJWfrhStuoJnLH6Wodbm7Uq0ELrdK0DOwQ1lalDI8t2+FvnGUZDHkHR+cv/AceIUhMzbpoU=', '::1', 'Mozilla/5.0 (iPhone; CPU iPhone OS 11_0 like Mac OS X) AppleWebKit/604.1.38 (KHTML, like Gecko) Version/11.0 Mobile/15A372 Safari/604.1', '2021-02-10 12:10:03', '1');
INSERT INTO `db_token` VALUES ('2850', '1', 'admin', 'DSpazZTPAw74/cO3CGcMWCQXt+sVyov5PHBfsOl5DnnPbWvgK7aaG5dj07/2+f7GjMrHHUqbk80ynbLjDR3mMZNg/Po0tpNtWYQgOpQQXE7MN9C8XSCNpHzmmjgmsVK6D1w8OJ5gIi9eaLx1ouXHVOcibRVdGnIbF6zEm6bLxW5n8buNjnlVzmt6rJ+Kaoq633NMpTfZfq5gj6wtSe84kg==', '::1', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/88.0.4324.146 Safari/537.36', '2021-02-10 12:10:06', '1');
INSERT INTO `db_token` VALUES ('2851', '1', 'admin', 'DSpazZTPAw74/cO3CGcMWCQXt+sVyov5PHBfsOl5DnnPbWvgK7aaG5dj07/2+f7GN2L7EVhH01Y42537BgYHv081kzMrjbEzrNM1iLIEfH2thY9m4AbI25oKmBZcmZdECdk+sJfrMQYpnheXJT21gy9fMKn1cXnHE4+Q7jHnQOGyUT2vPw4zmCFrkcqbTgQrg5uzBb1iPTXRdptaYibdOQwbZKti53hx0mFosS62DHw=', '::1', 'Mozilla/5.0 (Linux; Android 6.0; Nexus 5 Build/MRA58N) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/76.0.3809.132 Mobile Safari/537.36', '2021-02-10 12:10:28', '1');
INSERT INTO `db_token` VALUES ('2852', '1', 'admin', 'DSpazZTPAw74/cO3CGcMWCQXt+sVyov5PHBfsOl5DnnPbWvgK7aaG5dj07/2+f7Gsy4QC9fbnf3XJhDS3GLouuE6VzLc/YMx9+OD6Df44mkCGxtOTYectggi9Pzn+VRar3IvGIE0jCalDGDi7YrLm8PrNSSHXA4fmD1mfsWX+QkxESXTxwYmMRgV51XWRNYdwewajzsJXBDbaFKZ5dILVn6cGBf2BDhHfLzkL0zXBJ5lURKzqhHQCPr3iZfKRBWl', '::1', 'Mozilla/5.0 (iPhone; CPU iPhone OS 13_2_3 like Mac OS X) AppleWebKit/605.1.15 (KHTML, like Gecko) Version/13.0.3 Mobile/15E148 Safari/604.1', '2021-02-10 12:13:48', '1');
INSERT INTO `db_token` VALUES ('2854', '1', 'admin', 'DSpazZTPAw74/cO3CGcMWCQXt+sVyov5PHBfsOl5DnnPbWvgK7aaG5dj07/2+f7GjMrHHUqbk80ynbLjDR3mMZNg/Po0tpNtWYQgOpQQXE7MN9C8XSCNpHzmmjgmsVK6D1w8OJ5gIi9eaLx1ouXHVOcibRVdGnIbF6zEm6bLxW5n8buNjnlVzmt6rJ+Kaoq633NMpTfZfq5gj6wtSe84kg==', '::1', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/88.0.4324.146 Safari/537.36', '2021-02-10 12:26:44', '1');
INSERT INTO `db_token` VALUES ('2855', '1', 'admin', 'DSpazZTPAw74/cO3CGcMWCQXt+sVyov5PHBfsOl5DnnPbWvgK7aaG5dj07/2+f7Gsy4QC9fbnf3XJhDS3GLouuE6VzLc/YMx9+OD6Df44mkCGxtOTYectggi9Pzn+VRar3IvGIE0jCalDGDi7YrLm8PrNSSHXA4fmD1mfsWX+QkxESXTxwYmMRgV51XWRNYdwewajzsJXBDbaFKZ5dILVn6cGBf2BDhHfLzkL0zXBJ5lURKzqhHQCPr3iZfKRBWl', '::1', 'Mozilla/5.0 (iPhone; CPU iPhone OS 13_2_3 like Mac OS X) AppleWebKit/605.1.15 (KHTML, like Gecko) Version/13.0.3 Mobile/15E148 Safari/604.1', '2021-02-10 12:41:15', '1');
INSERT INTO `db_token` VALUES ('2856', '1', 'admin', 'DSpazZTPAw74/cO3CGcMWCQXt+sVyov5PHBfsOl5DnnPbWvgK7aaG5dj07/2+f7GjMrHHUqbk80ynbLjDR3mMZNg/Po0tpNtWYQgOpQQXE7MN9C8XSCNpHzmmjgmsVK6D1w8OJ5gIi9eaLx1ouXHVOcibRVdGnIbF6zEm6bLxW5n8buNjnlVzmt6rJ+Kaoq633NMpTfZfq5gj6wtSe84kg==', '::1', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/88.0.4324.146 Safari/537.36', '2021-02-10 12:43:25', '1');
INSERT INTO `db_token` VALUES ('2857', '1', 'admin', 'DSpazZTPAw74/cO3CGcMWCQXt+sVyov5PHBfsOl5DnnPbWvgK7aaG5dj07/2+f7GjMrHHUqbk80ynbLjDR3mMZNg/Po0tpNtWYQgOpQQXE7MN9C8XSCNpHzmmjgmsVK6D1w8OJ5gIi9eaLx1ouXHVKgmaOkn8y7g81SAxWp9IHLrS1l9hG+JGtpPDCh7lxJE33NMpTfZfq5gj6wtSe84kg==', '::1', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/76.0.3809.132 Safari/537.36', '2021-02-10 14:45:16', '1');
INSERT INTO `db_token` VALUES ('2859', '1', 'admin', 'DSpazZTPAw74/cO3CGcMWL/W44joPtjvCnS+6t2boTGqken+UeVUXCMZFrMJXeb0eVJfFIctEc5WpPadGkFHQUmHw3CPXNu5hLJL+Z02v8nenfOsILcB2GNNcNBW5tgrZfsnryf3sHFxFbwP1fjC/thGvwBW0IoAR09HmnwpUYx2p+fCe/tKRiAxaT3boVh8bhycxyaEncNHdmF2ns3izGPN5XlRJFS+mv3reUwPUbblDi8Y2As5kfqymWKPD705', '174.139.160.94', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/88.0.4324.150 Safari/537.36 Edg/88.0.705.63', '2021-02-10 19:31:45', '1');
INSERT INTO `db_token` VALUES ('2860', '1', 'admin', 'DSpazZTPAw74/cO3CGcMWL/W44joPtjvCnS+6t2boTGqken+UeVUXCMZFrMJXeb0YF1je+pC5UyJk7quOFSzTsSQrJhlDulU1J97GZAxUcEakbPpx+qldOCAix0ruokUEsOVaPEqNwIhGERlIrGOiJ6utUUojZWdYz9Exk/P4DBZiX2/gX3Gq3hTDIi/LXTV37TyNiVP/VTqd/8G2SWk/RFwp4gx93APf/EaYlLlmQeft6mmp8M3v33hrPYSex/qtxn9U+lbBlWZGK3lLnw4bw==', '174.139.160.94', 'Mozilla/5.0 (Linux; Android 6.0; Nexus 5 Build/MRA58N) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/88.0.4324.150 Mobile Safari/537.36 Edg/88.0.705.63', '2021-02-10 19:41:06', '1');
INSERT INTO `db_token` VALUES ('2861', '1', 'admin', 'DSpazZTPAw74/cO3CGcMWL/W44joPtjvCnS+6t2boTGqken+UeVUXCMZFrMJXeb0YF1je+pC5UyJk7quOFSzTsSQrJhlDulU1J97GZAxUcEakbPpx+qldOCAix0ruokUEsOVaPEqNwIhGERlIrGOiJ6utUUojZWdYz9Exk/P4DBZiX2/gX3Gq3hTDIi/LXTVASCssibA+PAo/1HvqozUXhFwp4gx93APf/EaYlLlmQdy0LAlu/7LQBk7H5UwsL5V', '174.139.160.94', 'Mozilla/5.0 (Linux; Android 6.0; Nexus 5 Build/MRA58N) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/76.0.3809.100 Mobile Safari/537.36', '2021-02-10 19:44:19', '1');
INSERT INTO `db_token` VALUES ('2862', '1', 'admin', 'DSpazZTPAw74/cO3CGcMWCQXt+sVyov5PHBfsOl5DnnPbWvgK7aaG5dj07/2+f7GjMrHHUqbk80ynbLjDR3mMZNg/Po0tpNtWYQgOpQQXE7MN9C8XSCNpHzmmjgmsVK6D1w8OJ5gIi9eaLx1ouXHVOcibRVdGnIbF6zEm6bLxW6CSL1qGMYrWOjH2rO1m2p633NMpTfZfq5gj6wtSe84kg==', '::1', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/88.0.4324.182 Safari/537.36', '2021-02-22 09:34:14', '1');
INSERT INTO `db_token` VALUES ('2863', '1', 'admin', 'DSpazZTPAw74/cO3CGcMWCQXt+sVyov5PHBfsOl5DnnPbWvgK7aaG5dj07/2+f7GN2L7EVhH01Y42537BgYHv081kzMrjbEzrNM1iLIEfH2thY9m4AbI25oKmBZcmZdECdk+sJfrMQYpnheXJT21gy9fMKn1cXnHE4+Q7jHnQOGyUT2vPw4zmCFrkcqbTgQrg5uzBb1iPTXRdptaYibdOQwbZKti53hx0mFosS62DHw=', '::1', 'Mozilla/5.0 (Linux; Android 6.0; Nexus 5 Build/MRA58N) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/76.0.3809.132 Mobile Safari/537.36', '2021-02-22 09:42:23', '1');
INSERT INTO `db_token` VALUES ('2866', '1', 'admin', 'DSpazZTPAw74/cO3CGcMWCQXt+sVyov5PHBfsOl5DnnPbWvgK7aaG5dj07/2+f7GjMrHHUqbk80ynbLjDR3mMZNg/Po0tpNtWYQgOpQQXE7MN9C8XSCNpHzmmjgmsVK6D1w8OJ5gIi9eaLx1ouXHVOcibRVdGnIbF6zEm6bLxW6CSL1qGMYrWOjH2rO1m2p633NMpTfZfq5gj6wtSe84kg==', '::1', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/88.0.4324.182 Safari/537.36', '2021-02-22 11:20:38', '1');
INSERT INTO `db_token` VALUES ('2868', '1', 'admin', 'DSpazZTPAw74/cO3CGcMWCQXt+sVyov5PHBfsOl5DnnPbWvgK7aaG5dj07/2+f7GN2L7EVhH01Y42537BgYHv081kzMrjbEzrNM1iLIEfH2thY9m4AbI25oKmBZcmZdECdk+sJfrMQYpnheXJT21gy9fMKn1cXnHE4+Q7jHnQOGyUT2vPw4zmCFrkcqbTgQrg5uzBb1iPTXRdptaYibdOQwbZKti53hx0mFosS62DHw=', '::1', 'Mozilla/5.0 (Linux; Android 6.0; Nexus 5 Build/MRA58N) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/76.0.3809.132 Mobile Safari/537.36', '2021-02-22 14:01:09', '1');
INSERT INTO `db_token` VALUES ('2869', '1', 'admin', 'DSpazZTPAw74/cO3CGcMWCQXt+sVyov5PHBfsOl5DnnPbWvgK7aaG5dj07/2+f7GjMrHHUqbk80ynbLjDR3mMZNg/Po0tpNtWYQgOpQQXE7MN9C8XSCNpHzmmjgmsVK6D1w8OJ5gIi9eaLx1ouXHVOcibRVdGnIbF6zEm6bLxW6CSL1qGMYrWOjH2rO1m2p633NMpTfZfq5gj6wtSe84kg==', '::1', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/88.0.4324.182 Safari/537.36', '2021-02-22 17:08:44', '1');
INSERT INTO `db_token` VALUES ('2871', '1', 'admin', 'DSpazZTPAw74/cO3CGcMWCQXt+sVyov5PHBfsOl5DnnPbWvgK7aaG5dj07/2+f7GjMrHHUqbk80ynbLjDR3mMZNg/Po0tpNtWYQgOpQQXE7MN9C8XSCNpHzmmjgmsVK6D1w8OJ5gIi9eaLx1ouXHVKgmaOkn8y7g81SAxWp9IHLrS1l9hG+JGtpPDCh7lxJE33NMpTfZfq5gj6wtSe84kg==', '::1', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/76.0.3809.132 Safari/537.36', '2021-02-23 10:18:42', '1');
INSERT INTO `db_token` VALUES ('2875', '1', 'admin', 'DSpazZTPAw74/cO3CGcMWCQXt+sVyov5PHBfsOl5DnnPbWvgK7aaG5dj07/2+f7GjMrHHUqbk80ynbLjDR3mMZNg/Po0tpNtWYQgOpQQXE7MN9C8XSCNpHzmmjgmsVK6D1w8OJ5gIi9eaLx1ouXHVOcibRVdGnIbF6zEm6bLxW6ZRlQC34qjIKbzvt5T+eTU33NMpTfZfq5gj6wtSe84kg==', '::1', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/88.0.4324.190 Safari/537.36', '2021-03-07 15:21:53', '1');
INSERT INTO `db_token` VALUES ('2882', '1', 'admin', 'DSpazZTPAw74/cO3CGcMWCQXt+sVyov5PHBfsOl5DnnPbWvgK7aaG5dj07/2+f7GN2L7EVhH01Y42537BgYHv081kzMrjbEzrNM1iLIEfH2thY9m4AbI25oKmBZcmZdECdk+sJfrMQYpnheXJT21gy9fMKn1cXnHE4+Q7jHnQOGyUT2vPw4zmCFrkcqbTgQrg5uzBb1iPTXRdptaYibdOQwbZKti53hx0mFosS62DHw=', '::1', 'Mozilla/5.0 (Linux; Android 6.0; Nexus 5 Build/MRA58N) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/76.0.3809.132 Mobile Safari/537.36', '2021-03-08 11:55:52', '1');
INSERT INTO `db_token` VALUES ('2885', '1', 'admin', 'DSpazZTPAw74/cO3CGcMWCQXt+sVyov5PHBfsOl5DnnPbWvgK7aaG5dj07/2+f7GN2L7EVhH01Y42537BgYHv081kzMrjbEzrNM1iLIEfH2thY9m4AbI25oKmBZcmZdECdk+sJfrMQYpnheXJT21gy9fMKn1cXnHE4+Q7jHnQOGyUT2vPw4zmCFrkcqbTgQrg5uzBb1iPTXRdptaYibdOQwbZKti53hx0mFosS62DHw=', '::1', 'Mozilla/5.0 (Linux; Android 6.0; Nexus 5 Build/MRA58N) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/76.0.3809.132 Mobile Safari/537.36', '2021-03-13 10:31:01', '1');
INSERT INTO `db_token` VALUES ('2890', '1', 'admin', 'DSpazZTPAw74/cO3CGcMWEdWe9A+8RH5mzWA5Y7rrJ5HIf+jUGUD4rqzY2fuj09k03kT/yFgkUpuPNHcS2Inc5sUnref3+IIv8owkYRHJPRzWexxj8PkvPfGLs+eClav4E4WcQYwkuox4QqK+4NCLjEUPj9VErRBb6+EbhHIvXTqgyYiK0Mt/O44DPZ7jAXh7C7/W7QJ7HLXbTo/e9Kc1oQJRQZqkv67A2FXHHqrPhTMPIyXGTaTvw2HerFcmqH7', '127.0.0.1', 'Mozilla/5.0 (Linux; Android 6.0; Nexus 5 Build/MRA58N) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/76.0.3809.132 Mobile Safari/537.36', '2021-03-13 15:57:16', '1');
INSERT INTO `db_token` VALUES ('2891', '1', 'admin', 'DSpazZTPAw74/cO3CGcMWCQXt+sVyov5PHBfsOl5DnnPbWvgK7aaG5dj07/2+f7GN2L7EVhH01Y42537BgYHv081kzMrjbEzrNM1iLIEfH2thY9m4AbI25oKmBZcmZdECdk+sJfrMQYpnheXJT21gy9fMKn1cXnHE4+Q7jHnQOGyUT2vPw4zmCFrkcqbTgQrg5uzBb1iPTXRdptaYibdOQwbZKti53hx0mFosS62DHw=', '::1', 'Mozilla/5.0 (Linux; Android 6.0; Nexus 5 Build/MRA58N) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/76.0.3809.132 Mobile Safari/537.36', '2021-03-13 16:04:03', '1');
INSERT INTO `db_token` VALUES ('2893', '1', 'admin', 'DSpazZTPAw74/cO3CGcMWCQXt+sVyov5PHBfsOl5DnnPbWvgK7aaG5dj07/2+f7GN2L7EVhH01Y42537BgYHv081kzMrjbEzrNM1iLIEfH2thY9m4AbI25oKmBZcmZdECdk+sJfrMQYpnheXJT21gy9fMKn1cXnHE4+Q7jHnQOGyUT2vPw4zmCFrkcqbTgQrg5uzBb1iPTXRdptaYibdOQwbZKti53hx0mFosS62DHw=', '::1', 'Mozilla/5.0 (Linux; Android 6.0; Nexus 5 Build/MRA58N) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/76.0.3809.132 Mobile Safari/537.36', '2021-03-13 17:45:08', '1');
INSERT INTO `db_token` VALUES ('2895', '1', 'admin', 'DSpazZTPAw74/cO3CGcMWCQXt+sVyov5PHBfsOl5DnnPbWvgK7aaG5dj07/2+f7GN2L7EVhH01Y42537BgYHv081kzMrjbEzrNM1iLIEfH2thY9m4AbI25oKmBZcmZdECdk+sJfrMQYpnheXJT21gy9fMKn1cXnHE4+Q7jHnQOGyUT2vPw4zmCFrkcqbTgQrg5uzBb1iPTXRdptaYibdOQwbZKti53hx0mFosS62DHw=', '::1', 'Mozilla/5.0 (Linux; Android 6.0; Nexus 5 Build/MRA58N) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/76.0.3809.132 Mobile Safari/537.36', '2021-03-13 18:06:42', '1');
INSERT INTO `db_token` VALUES ('2897', '1', 'admin', 'DSpazZTPAw74/cO3CGcMWCQXt+sVyov5PHBfsOl5DnnPbWvgK7aaG5dj07/2+f7GN2L7EVhH01Y42537BgYHv081kzMrjbEzrNM1iLIEfH2thY9m4AbI25oKmBZcmZdECdk+sJfrMQYpnheXJT21gy9fMKn1cXnHE4+Q7jHnQOGyUT2vPw4zmCFrkcqbTgQrg5uzBb1iPTXRdptaYibdOQwbZKti53hx0mFosS62DHw=', '::1', 'Mozilla/5.0 (Linux; Android 6.0; Nexus 5 Build/MRA58N) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/76.0.3809.132 Mobile Safari/537.36', '2021-03-14 09:33:47', '1');
INSERT INTO `db_token` VALUES ('2900', '1', 'admin', 'DSpazZTPAw74/cO3CGcMWCQXt+sVyov5PHBfsOl5DnnPbWvgK7aaG5dj07/2+f7GN2L7EVhH01Y42537BgYHv081kzMrjbEzrNM1iLIEfH2thY9m4AbI25oKmBZcmZdECdk+sJfrMQYpnheXJT21gy9fMKn1cXnHE4+Q7jHnQOGyUT2vPw4zmCFrkcqbTgQrg5uzBb1iPTXRdptaYibdOQwbZKti53hx0mFosS62DHw=', '::1', 'Mozilla/5.0 (Linux; Android 6.0; Nexus 5 Build/MRA58N) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/76.0.3809.132 Mobile Safari/537.36', '2021-03-14 09:49:45', '1');
INSERT INTO `db_token` VALUES ('2902', '1', 'admin', 'DSpazZTPAw74/cO3CGcMWCQXt+sVyov5PHBfsOl5DnnPbWvgK7aaG5dj07/2+f7GN2L7EVhH01Y42537BgYHv081kzMrjbEzrNM1iLIEfH2thY9m4AbI25oKmBZcmZdECdk+sJfrMQYpnheXJT21gy9fMKn1cXnHE4+Q7jHnQOGyUT2vPw4zmCFrkcqbTgQrg5uzBb1iPTXRdptaYibdOQwbZKti53hx0mFosS62DHw=', '::1', 'Mozilla/5.0 (Linux; Android 6.0; Nexus 5 Build/MRA58N) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/76.0.3809.132 Mobile Safari/537.36', '2021-03-14 10:02:45', '1');
INSERT INTO `db_token` VALUES ('2904', '1', 'admin', 'DSpazZTPAw74/cO3CGcMWCQXt+sVyov5PHBfsOl5DnnPbWvgK7aaG5dj07/2+f7GN2L7EVhH01Y42537BgYHv081kzMrjbEzrNM1iLIEfH2thY9m4AbI25oKmBZcmZdECdk+sJfrMQYpnheXJT21gy9fMKn1cXnHE4+Q7jHnQOGyUT2vPw4zmCFrkcqbTgQrg5uzBb1iPTXRdptaYibdOQwbZKti53hx0mFosS62DHw=', '::1', 'Mozilla/5.0 (Linux; Android 6.0; Nexus 5 Build/MRA58N) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/76.0.3809.132 Mobile Safari/537.36', '2021-03-14 10:06:00', '1');
INSERT INTO `db_token` VALUES ('2906', '1', 'admin', 'DSpazZTPAw74/cO3CGcMWCQXt+sVyov5PHBfsOl5DnnPbWvgK7aaG5dj07/2+f7GN2L7EVhH01Y42537BgYHv081kzMrjbEzrNM1iLIEfH2thY9m4AbI25oKmBZcmZdECdk+sJfrMQYpnheXJT21gy9fMKn1cXnHE4+Q7jHnQOGyUT2vPw4zmCFrkcqbTgQrg5uzBb1iPTXRdptaYibdOQwbZKti53hx0mFosS62DHw=', '::1', 'Mozilla/5.0 (Linux; Android 6.0; Nexus 5 Build/MRA58N) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/76.0.3809.132 Mobile Safari/537.36', '2021-03-14 10:08:01', '1');
INSERT INTO `db_token` VALUES ('2908', '1', 'admin', 'DSpazZTPAw74/cO3CGcMWCQXt+sVyov5PHBfsOl5DnnPbWvgK7aaG5dj07/2+f7GN2L7EVhH01Y42537BgYHv081kzMrjbEzrNM1iLIEfH2thY9m4AbI25oKmBZcmZdECdk+sJfrMQYpnheXJT21gy9fMKn1cXnHE4+Q7jHnQOGyUT2vPw4zmCFrkcqbTgQrg5uzBb1iPTXRdptaYibdOQwbZKti53hx0mFosS62DHw=', '::1', 'Mozilla/5.0 (Linux; Android 6.0; Nexus 5 Build/MRA58N) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/76.0.3809.132 Mobile Safari/537.36', '2021-03-14 10:09:19', '1');
INSERT INTO `db_token` VALUES ('2912', '1', 'admin', 'DSpazZTPAw74/cO3CGcMWCQXt+sVyov5PHBfsOl5DnnPbWvgK7aaG5dj07/2+f7GN2L7EVhH01Y42537BgYHv081kzMrjbEzrNM1iLIEfH2thY9m4AbI25oKmBZcmZdECdk+sJfrMQYpnheXJT21gy9fMKn1cXnHE4+Q7jHnQOGyUT2vPw4zmCFrkcqbTgQrg5uzBb1iPTXRdptaYibdOQwbZKti53hx0mFosS62DHw=', '::1', 'Mozilla/5.0 (Linux; Android 6.0; Nexus 5 Build/MRA58N) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/76.0.3809.132 Mobile Safari/537.36', '2021-03-14 10:25:32', '1');
INSERT INTO `db_token` VALUES ('2914', '1', 'admin', 'DSpazZTPAw74/cO3CGcMWCQXt+sVyov5PHBfsOl5DnnPbWvgK7aaG5dj07/2+f7GN2L7EVhH01Y42537BgYHv081kzMrjbEzrNM1iLIEfH2thY9m4AbI25oKmBZcmZdECdk+sJfrMQYpnheXJT21gy9fMKn1cXnHE4+Q7jHnQOGyUT2vPw4zmCFrkcqbTgQrg5uzBb1iPTXRdptaYibdOQwbZKti53hx0mFosS62DHw=', '::1', 'Mozilla/5.0 (Linux; Android 6.0; Nexus 5 Build/MRA58N) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/76.0.3809.132 Mobile Safari/537.36', '2021-03-14 10:27:17', '1');
INSERT INTO `db_token` VALUES ('2916', '1', 'admin', 'DSpazZTPAw74/cO3CGcMWCQXt+sVyov5PHBfsOl5DnnPbWvgK7aaG5dj07/2+f7GN2L7EVhH01Y42537BgYHv081kzMrjbEzrNM1iLIEfH2thY9m4AbI25oKmBZcmZdECdk+sJfrMQYpnheXJT21gy9fMKn1cXnHE4+Q7jHnQOGyUT2vPw4zmCFrkcqbTgQrg5uzBb1iPTXRdptaYibdOQwbZKti53hx0mFosS62DHw=', '::1', 'Mozilla/5.0 (Linux; Android 6.0; Nexus 5 Build/MRA58N) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/76.0.3809.132 Mobile Safari/537.36', '2021-03-14 10:37:57', '1');
INSERT INTO `db_token` VALUES ('2918', '1', 'admin', 'DSpazZTPAw74/cO3CGcMWCQXt+sVyov5PHBfsOl5DnnPbWvgK7aaG5dj07/2+f7GN2L7EVhH01Y42537BgYHv081kzMrjbEzrNM1iLIEfH2thY9m4AbI25oKmBZcmZdECdk+sJfrMQYpnheXJT21gy9fMKn1cXnHE4+Q7jHnQOGyUT2vPw4zmCFrkcqbTgQrg5uzBb1iPTXRdptaYibdOQwbZKti53hx0mFosS62DHw=', '::1', 'Mozilla/5.0 (Linux; Android 6.0; Nexus 5 Build/MRA58N) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/76.0.3809.132 Mobile Safari/537.36', '2021-03-14 10:40:54', '1');
INSERT INTO `db_token` VALUES ('2919', '1', 'admin', 'DSpazZTPAw74/cO3CGcMWCQXt+sVyov5PHBfsOl5DnnPbWvgK7aaG5dj07/2+f7GN2L7EVhH01Y42537BgYHv081kzMrjbEzrNM1iLIEfH2thY9m4AbI25oKmBZcmZdECdk+sJfrMQYpnheXJT21gy9fMKn1cXnHE4+Q7jHnQOGyUT2vPw4zmCFrkcqbTgQrg5uzBb1iPTXRdptaYibdOQwbZKti53hx0mFosS62DHw=', '::1', 'Mozilla/5.0 (Linux; Android 6.0; Nexus 5 Build/MRA58N) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/76.0.3809.132 Mobile Safari/537.36', '2021-03-14 10:44:22', '1');
INSERT INTO `db_token` VALUES ('2922', '1', 'admin', 'DSpazZTPAw74/cO3CGcMWCQXt+sVyov5PHBfsOl5DnnPbWvgK7aaG5dj07/2+f7GjMrHHUqbk80ynbLjDR3mMZNg/Po0tpNtWYQgOpQQXE7MN9C8XSCNpHzmmjgmsVK6D1w8OJ5gIi9eaLx1ouXHVFsfnRh+ICk6aEFZ63vkjrOJfoyfV5J4WdCh1b8XJmxdctCwJbv+y0AZOx+VMLC+VQ==', '::1', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/89.0.4389.82 Safari/537.36', '2021-03-14 10:52:27', '1');
INSERT INTO `db_token` VALUES ('2924', '1', 'admin', 'DSpazZTPAw74/cO3CGcMWCQXt+sVyov5PHBfsOl5DnnPbWvgK7aaG5dj07/2+f7GN2L7EVhH01Y42537BgYHv081kzMrjbEzrNM1iLIEfH2thY9m4AbI25oKmBZcmZdECdk+sJfrMQYpnheXJT21gy9fMKn1cXnHE4+Q7jHnQOGyUT2vPw4zmCFrkcqbTgQrg5uzBb1iPTXRdptaYibdOQwbZKti53hx0mFosS62DHw=', '::1', 'Mozilla/5.0 (Linux; Android 6.0; Nexus 5 Build/MRA58N) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/76.0.3809.132 Mobile Safari/537.36', '2021-03-14 14:51:31', '1');
INSERT INTO `db_token` VALUES ('2930', '1', 'admin', 'DSpazZTPAw74/cO3CGcMWCQXt+sVyov5PHBfsOl5DnnPbWvgK7aaG5dj07/2+f7GjMrHHUqbk80ynbLjDR3mMZNg/Po0tpNtWYQgOpQQXE7MN9C8XSCNpHzmmjgmsVK6D1w8OJ5gIi9eaLx1ouXHVFsfnRh+ICk6aEFZ63vkjrOP2CGpmML41eWAyRzGI+U4ctCwJbv+y0AZOx+VMLC+VQ==', '::1', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/89.0.4389.90 Safari/537.36', '2021-03-27 09:54:38', '1');
INSERT INTO `db_token` VALUES ('2931', '1', 'admin', 'DSpazZTPAw74/cO3CGcMWHl1dS07H2lCWkRAs7FkWjWn7bbRMt9CkNAUT5uC+AnUHAcLh65Pzj8HpcEippTRrroeiFWBhryEw02lQ5sMNlafnKXJSodu6KOWK5oE81l4SE4bN1kNJVXJJ9mREn8tN/lAyRcl5pI4/RGROzV6XdHPlnfDcxZIlYlGvPVur3OHXmVTwTRWVoudA9mTmsmauY3Q224bWhz0NSXsc2QVJEqeczyO12YlF0o6ytAs/K0h', '180.140.160.167', 'Mozilla/5.0 (iPhone; CPU iPhone OS 14_1 like Mac OS X) AppleWebKit/605.1.15 (KHTML, like Gecko) Version/14.0 Mobile/15E148 Safari/604.1', '2021-03-27 10:19:31', '1');
INSERT INTO `db_token` VALUES ('2932', '1', 'admin', 'DSpazZTPAw74/cO3CGcMWCderMW2o7j+PnKRYlemoPJCgthNQZfU49q01H0Bgg1RYF1je+pC5UyJk7quOFSzToVRw4frRTTklNcmtgQkEb3qw+DpkcJtKY8LS1SXxQjQemMm3VIw7FSl63eHmnx9gvrMw4Rc5xKfSyi1K5lZjU7MN9C8XSCNpHzmmjgmsVK6D1w8OJ5gIi9eaLx1ouXHVBwhTaxylliDIflokgJz2t+uhXR8/G7e5vJQ3HwxXto6L0jeAJw9GGRav1udkPGOj7w9Q6bvygwp6sr1vS77Oyu492PnKWwQ2SjnzbXcJ3dZ', '106.127.197.80', 'Mozilla/5.0 (Linux; U; Android 10; zh-CN; Redmi K20 Pro Build/QKQ1.190825.002) AppleWebKit/537.36 (KHTML, like Gecko) Version/4.0 Chrome/78.0.3904.108 Quark/4.5.6.156 Mobile Safari/537.36', '2021-03-27 10:21:01', '1');
INSERT INTO `db_token` VALUES ('2933', '1', 'admin', 'DSpazZTPAw74/cO3CGcMWCQXt+sVyov5PHBfsOl5DnnPbWvgK7aaG5dj07/2+f7GjMrHHUqbk80ynbLjDR3mMZNg/Po0tpNtWYQgOpQQXE7MN9C8XSCNpHzmmjgmsVK6D1w8OJ5gIi9eaLx1ouXHVFsfnRh+ICk6aEFZ63vkjrOP2CGpmML41eWAyRzGI+U4ctCwJbv+y0AZOx+VMLC+VQ==', '::1', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/89.0.4389.90 Safari/537.36', '2021-03-27 10:29:53', '1');
INSERT INTO `db_token` VALUES ('2934', '1', 'admin', 'DSpazZTPAw74/cO3CGcMWCQXt+sVyov5PHBfsOl5DnnPbWvgK7aaG5dj07/2+f7GjMrHHUqbk80ynbLjDR3mMZNg/Po0tpNtWYQgOpQQXE7MN9C8XSCNpHzmmjgmsVK6D1w8OJ5gIi9eaLx1ouXHVFsfnRh+ICk6aEFZ63vkjrOP2CGpmML41eWAyRzGI+U4ctCwJbv+y0AZOx+VMLC+VQ==', '::1', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/89.0.4389.90 Safari/537.36', '2021-03-27 10:40:54', '1');
INSERT INTO `db_token` VALUES ('2935', '1', 'admin', 'DSpazZTPAw74/cO3CGcMWCQXt+sVyov5PHBfsOl5DnnPbWvgK7aaG5dj07/2+f7GjMrHHUqbk80ynbLjDR3mMZNg/Po0tpNtWYQgOpQQXE7MN9C8XSCNpHzmmjgmsVK6D1w8OJ5gIi9eaLx1ouXHVFsfnRh+ICk6aEFZ63vkjrOP2CGpmML41eWAyRzGI+U4ctCwJbv+y0AZOx+VMLC+VQ==', '::1', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/89.0.4389.90 Safari/537.36', '2021-03-27 10:41:01', '1');
INSERT INTO `db_token` VALUES ('2936', '1', 'admin', 'DSpazZTPAw74/cO3CGcMWCQXt+sVyov5PHBfsOl5DnnPbWvgK7aaG5dj07/2+f7GjMrHHUqbk80ynbLjDR3mMZNg/Po0tpNtWYQgOpQQXE7MN9C8XSCNpHzmmjgmsVK6D1w8OJ5gIi9eaLx1ouXHVFsfnRh+ICk6aEFZ63vkjrOP2CGpmML41eWAyRzGI+U4ctCwJbv+y0AZOx+VMLC+VQ==', '::1', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/89.0.4389.90 Safari/537.36', '2021-03-27 10:43:00', '1');
INSERT INTO `db_token` VALUES ('2937', '1', 'admin', 'DSpazZTPAw74/cO3CGcMWCQXt+sVyov5PHBfsOl5DnnPbWvgK7aaG5dj07/2+f7GjMrHHUqbk80ynbLjDR3mMZNg/Po0tpNtWYQgOpQQXE7MN9C8XSCNpHzmmjgmsVK6D1w8OJ5gIi9eaLx1ouXHVFsfnRh+ICk6aEFZ63vkjrOP2CGpmML41eWAyRzGI+U4ctCwJbv+y0AZOx+VMLC+VQ==', '::1', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/89.0.4389.90 Safari/537.36', '2021-03-27 10:43:35', '1');
INSERT INTO `db_token` VALUES ('2938', '1', 'admin', 'DSpazZTPAw74/cO3CGcMWCQXt+sVyov5PHBfsOl5DnnPbWvgK7aaG5dj07/2+f7GjMrHHUqbk80ynbLjDR3mMZNg/Po0tpNtWYQgOpQQXE7MN9C8XSCNpHzmmjgmsVK6D1w8OJ5gIi9eaLx1ouXHVFsfnRh+ICk6aEFZ63vkjrOP2CGpmML41eWAyRzGI+U4ctCwJbv+y0AZOx+VMLC+VQ==', '::1', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/89.0.4389.90 Safari/537.36', '2021-03-27 10:47:04', '1');
INSERT INTO `db_token` VALUES ('2939', '1', 'admin', 'DSpazZTPAw74/cO3CGcMWCQXt+sVyov5PHBfsOl5DnnPbWvgK7aaG5dj07/2+f7GjMrHHUqbk80ynbLjDR3mMZNg/Po0tpNtWYQgOpQQXE7MN9C8XSCNpHzmmjgmsVK6D1w8OJ5gIi9eaLx1ouXHVFsfnRh+ICk6aEFZ63vkjrOP2CGpmML41eWAyRzGI+U4ctCwJbv+y0AZOx+VMLC+VQ==', '::1', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/89.0.4389.90 Safari/537.36', '2021-03-27 10:51:47', '1');
INSERT INTO `db_token` VALUES ('2940', '1', 'admin', 'DSpazZTPAw74/cO3CGcMWHl1dS07H2lCWkRAs7FkWjWn7bbRMt9CkNAUT5uC+AnUHAcLh65Pzj8HpcEippTRrroeiFWBhryEw02lQ5sMNlafnKXJSodu6KOWK5oE81l4SE4bN1kNJVXJJ9mREn8tN/lAyRcl5pI4/RGROzV6XdHPlnfDcxZIlYlGvPVur3OHXmVTwTRWVoudA9mTmsmauY3Q224bWhz0NSXsc2QVJEqeczyO12YlF0o6ytAs/K0h', '180.140.160.167', 'Mozilla/5.0 (iPhone; CPU iPhone OS 14_1 like Mac OS X) AppleWebKit/605.1.15 (KHTML, like Gecko) Version/14.0 Mobile/15E148 Safari/604.1', '2021-03-27 10:55:41', '1');
INSERT INTO `db_token` VALUES ('2941', '1', 'admin', 'DSpazZTPAw74/cO3CGcMWHl1dS07H2lCWkRAs7FkWjWn7bbRMt9CkNAUT5uC+AnUCnZ87m0DlLkHxDDBUQ5zQDGC4QunO76cObJm4MewneMCtTJ3yRrd8TnyI9rTaWU8fWu9rKRppzqbM9nWkB3gXhDK/SysMzC14raYxTfcaqrDJUDtcwYA2KxEY4RJIDFhbpK8sl9fZr0SBA/RJyX0gGJU+1U09HtxLzYTH8SpeQE=', '180.140.160.167', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/89.0.4389.90 Safari/537.36', '2021-03-27 10:58:23', '1');
INSERT INTO `db_token` VALUES ('2942', '1', 'admin', 'DSpazZTPAw74/cO3CGcMWHl1dS07H2lCWkRAs7FkWjWn7bbRMt9CkNAUT5uC+AnUCnZ87m0DlLkHxDDBUQ5zQDGC4QunO76cObJm4MewneMCtTJ3yRrd8TnyI9rTaWU8fWu9rKRppzqbM9nWkB3gXhDK/SysMzC14raYxTfcaqrDJUDtcwYA2KxEY4RJIDFhbpK8sl9fZr0SBA/RJyX0gGJU+1U09HtxLzYTH8SpeQE=', '180.140.160.167', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/89.0.4389.90 Safari/537.36', '2021-03-27 10:59:00', '1');
INSERT INTO `db_token` VALUES ('2943', '1', 'admin', 'DSpazZTPAw74/cO3CGcMWHl1dS07H2lCWkRAs7FkWjWn7bbRMt9CkNAUT5uC+AnUCnZ87m0DlLkHxDDBUQ5zQDGC4QunO76cObJm4MewneMCtTJ3yRrd8TnyI9rTaWU8fWu9rKRppzqbM9nWkB3gXhDK/SysMzC14raYxTfcaqrDJUDtcwYA2KxEY4RJIDFhbpK8sl9fZr0SBA/RJyX0gGJU+1U09HtxLzYTH8SpeQE=', '180.140.160.167', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/89.0.4389.90 Safari/537.36', '2021-03-27 11:03:19', '1');
INSERT INTO `db_token` VALUES ('2944', '1', 'admin', 'DSpazZTPAw74/cO3CGcMWCQXt+sVyov5PHBfsOl5DnnPbWvgK7aaG5dj07/2+f7GjMrHHUqbk80ynbLjDR3mMZNg/Po0tpNtWYQgOpQQXE7MN9C8XSCNpHzmmjgmsVK6D1w8OJ5gIi9eaLx1ouXHVFsfnRh+ICk6aEFZ63vkjrOP2CGpmML41eWAyRzGI+U4ctCwJbv+y0AZOx+VMLC+VQ==', '::1', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/89.0.4389.90 Safari/537.36', '2021-03-27 11:10:22', '1');
INSERT INTO `db_token` VALUES ('2945', '1', 'admin', 'DSpazZTPAw74/cO3CGcMWCQXt+sVyov5PHBfsOl5DnnPbWvgK7aaG5dj07/2+f7GjMrHHUqbk80ynbLjDR3mMZNg/Po0tpNtWYQgOpQQXE7MN9C8XSCNpHzmmjgmsVK6D1w8OJ5gIi9eaLx1ouXHVFsfnRh+ICk6aEFZ63vkjrOP2CGpmML41eWAyRzGI+U4ctCwJbv+y0AZOx+VMLC+VQ==', '::1', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/89.0.4389.90 Safari/537.36', '2021-03-27 11:11:51', '1');
INSERT INTO `db_token` VALUES ('2946', '1', 'admin', 'DSpazZTPAw74/cO3CGcMWCQXt+sVyov5PHBfsOl5DnnPbWvgK7aaG5dj07/2+f7GjMrHHUqbk80ynbLjDR3mMZNg/Po0tpNtWYQgOpQQXE7MN9C8XSCNpHzmmjgmsVK6D1w8OJ5gIi9eaLx1ouXHVFsfnRh+ICk6aEFZ63vkjrOP2CGpmML41eWAyRzGI+U4ctCwJbv+y0AZOx+VMLC+VQ==', '::1', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/89.0.4389.90 Safari/537.36', '2021-03-27 11:16:17', '1');
INSERT INTO `db_token` VALUES ('2947', '1', 'admin', 'DSpazZTPAw74/cO3CGcMWCQXt+sVyov5PHBfsOl5DnnPbWvgK7aaG5dj07/2+f7GjMrHHUqbk80ynbLjDR3mMZNg/Po0tpNtWYQgOpQQXE7MN9C8XSCNpHzmmjgmsVK6D1w8OJ5gIi9eaLx1ouXHVFsfnRh+ICk6aEFZ63vkjrOP2CGpmML41eWAyRzGI+U4ctCwJbv+y0AZOx+VMLC+VQ==', '::1', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/89.0.4389.90 Safari/537.36', '2021-03-27 11:21:05', '1');
INSERT INTO `db_token` VALUES ('2948', '1', 'admin', 'DSpazZTPAw74/cO3CGcMWHl1dS07H2lCWkRAs7FkWjWn7bbRMt9CkNAUT5uC+AnUHAcLh65Pzj8HpcEippTRrroeiFWBhryEw02lQ5sMNlafnKXJSodu6KOWK5oE81l4SE4bN1kNJVXJJ9mREn8tN/lAyRcl5pI4/RGROzV6XdHPlnfDcxZIlYlGvPVur3OHXmVTwTRWVoudA9mTmsmauY3Q224bWhz0NSXsc2QVJEqeczyO12YlF0o6ytAs/K0h', '180.140.160.167', 'Mozilla/5.0 (iPhone; CPU iPhone OS 14_1 like Mac OS X) AppleWebKit/605.1.15 (KHTML, like Gecko) Version/14.0 Mobile/15E148 Safari/604.1', '2021-03-27 11:26:30', '1');
INSERT INTO `db_token` VALUES ('2949', '1', 'admin', 'DSpazZTPAw74/cO3CGcMWCQXt+sVyov5PHBfsOl5DnnPbWvgK7aaG5dj07/2+f7GjMrHHUqbk80ynbLjDR3mMZNg/Po0tpNtWYQgOpQQXE7MN9C8XSCNpHzmmjgmsVK6D1w8OJ5gIi9eaLx1ouXHVFsfnRh+ICk6aEFZ63vkjrOP2CGpmML41eWAyRzGI+U4ctCwJbv+y0AZOx+VMLC+VQ==', '::1', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/89.0.4389.90 Safari/537.36', '2021-03-27 11:31:11', '1');
INSERT INTO `db_token` VALUES ('2950', '1', 'admin', 'DSpazZTPAw74/cO3CGcMWCQXt+sVyov5PHBfsOl5DnnPbWvgK7aaG5dj07/2+f7GjMrHHUqbk80ynbLjDR3mMZNg/Po0tpNtWYQgOpQQXE7MN9C8XSCNpHzmmjgmsVK6D1w8OJ5gIi9eaLx1ouXHVFsfnRh+ICk6aEFZ63vkjrOP2CGpmML41eWAyRzGI+U4ctCwJbv+y0AZOx+VMLC+VQ==', '::1', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/89.0.4389.90 Safari/537.36', '2021-03-27 11:31:28', '1');
INSERT INTO `db_token` VALUES ('2951', '1', 'admin', 'DSpazZTPAw74/cO3CGcMWHl1dS07H2lCWkRAs7FkWjWn7bbRMt9CkNAUT5uC+AnUHAcLh65Pzj8HpcEippTRrroeiFWBhryEw02lQ5sMNlafnKXJSodu6KOWK5oE81l4SE4bN1kNJVXJJ9mREn8tN/lAyRcl5pI4/RGROzV6XdHPlnfDcxZIlYlGvPVur3OHXmVTwTRWVoudA9mTmsmauY3Q224bWhz0NSXsc2QVJEqeczyO12YlF0o6ytAs/K0h', '180.140.160.167', 'Mozilla/5.0 (iPhone; CPU iPhone OS 14_1 like Mac OS X) AppleWebKit/605.1.15 (KHTML, like Gecko) Version/14.0 Mobile/15E148 Safari/604.1', '2021-03-27 11:34:19', '1');
INSERT INTO `db_token` VALUES ('2952', '1', 'admin', 'DSpazZTPAw74/cO3CGcMWHl1dS07H2lCWkRAs7FkWjWn7bbRMt9CkNAUT5uC+AnUHAcLh65Pzj8HpcEippTRrroeiFWBhryEw02lQ5sMNlafnKXJSodu6KOWK5oE81l4SE4bN1kNJVXJJ9mREn8tN/lAyRcl5pI4/RGROzV6XdHPlnfDcxZIlYlGvPVur3OHXmVTwTRWVoudA9mTmsmauY3Q224bWhz0NSXsc2QVJEqeczyO12YlF0o6ytAs/K0h', '180.140.160.167', 'Mozilla/5.0 (iPhone; CPU iPhone OS 14_1 like Mac OS X) AppleWebKit/605.1.15 (KHTML, like Gecko) Version/14.0 Mobile/15E148 Safari/604.1', '2021-03-27 11:37:41', '1');
INSERT INTO `db_token` VALUES ('2953', '1', 'admin', 'DSpazZTPAw74/cO3CGcMWCQXt+sVyov5PHBfsOl5DnnPbWvgK7aaG5dj07/2+f7Gsy4QC9fbnf3XJhDS3GLouuE6VzLc/YMx9+OD6Df44mkCGxtOTYectggi9Pzn+VRar3IvGIE0jCalDGDi7YrLm8PrNSSHXA4fmD1mfsWX+QkxESXTxwYmMRgV51XWRNYdwewajzsJXBDbaFKZ5dILVn6cGBf2BDhHfLzkL0zXBJ5lURKzqhHQCPr3iZfKRBWl', '::1', 'Mozilla/5.0 (iPhone; CPU iPhone OS 13_2_3 like Mac OS X) AppleWebKit/605.1.15 (KHTML, like Gecko) Version/13.0.3 Mobile/15E148 Safari/604.1', '2021-03-27 11:38:52', '1');
INSERT INTO `db_token` VALUES ('2954', '1', 'admin', 'DSpazZTPAw74/cO3CGcMWCQXt+sVyov5PHBfsOl5DnnPbWvgK7aaG5dj07/2+f7Gsy4QC9fbnf3XJhDS3GLouuE6VzLc/YMx9+OD6Df44mkCGxtOTYectggi9Pzn+VRar3IvGIE0jCalDGDi7YrLm8PrNSSHXA4fmD1mfsWX+QkxESXTxwYmMRgV51XWRNYdwewajzsJXBDbaFKZ5dILVn6cGBf2BDhHfLzkL0zXBJ5lURKzqhHQCPr3iZfKRBWl', '::1', 'Mozilla/5.0 (iPhone; CPU iPhone OS 13_2_3 like Mac OS X) AppleWebKit/605.1.15 (KHTML, like Gecko) Version/13.0.3 Mobile/15E148 Safari/604.1', '2021-03-27 11:41:13', '1');
INSERT INTO `db_token` VALUES ('2955', '1', 'admin', 'DSpazZTPAw74/cO3CGcMWCQXt+sVyov5PHBfsOl5DnnPbWvgK7aaG5dj07/2+f7Gsy4QC9fbnf3XJhDS3GLouuE6VzLc/YMx9+OD6Df44mkCGxtOTYectggi9Pzn+VRar3IvGIE0jCalDGDi7YrLm8PrNSSHXA4fmD1mfsWX+QkxESXTxwYmMRgV51XWRNYdwewajzsJXBDbaFKZ5dILVn6cGBf2BDhHfLzkL0zXBJ5lURKzqhHQCPr3iZfKRBWl', '::1', 'Mozilla/5.0 (iPhone; CPU iPhone OS 13_2_3 like Mac OS X) AppleWebKit/605.1.15 (KHTML, like Gecko) Version/13.0.3 Mobile/15E148 Safari/604.1', '2021-03-27 11:42:49', '1');
INSERT INTO `db_token` VALUES ('2956', '1', 'admin', 'DSpazZTPAw74/cO3CGcMWCQXt+sVyov5PHBfsOl5DnnPbWvgK7aaG5dj07/2+f7Gsy4QC9fbnf3XJhDS3GLouuE6VzLc/YMx9+OD6Df44mkCGxtOTYectggi9Pzn+VRar3IvGIE0jCalDGDi7YrLm8PrNSSHXA4fmD1mfsWX+QkxESXTxwYmMRgV51XWRNYdwewajzsJXBDbaFKZ5dILVn6cGBf2BDhHfLzkL0zXBJ5lURKzqhHQCPr3iZfKRBWl', '::1', 'Mozilla/5.0 (iPhone; CPU iPhone OS 13_2_3 like Mac OS X) AppleWebKit/605.1.15 (KHTML, like Gecko) Version/13.0.3 Mobile/15E148 Safari/604.1', '2021-03-27 11:46:11', '1');
INSERT INTO `db_token` VALUES ('2957', '1', 'admin', 'DSpazZTPAw74/cO3CGcMWCQXt+sVyov5PHBfsOl5DnnPbWvgK7aaG5dj07/2+f7Gsy4QC9fbnf3XJhDS3GLouuE6VzLc/YMx9+OD6Df44mkCGxtOTYectggi9Pzn+VRar3IvGIE0jCalDGDi7YrLm8PrNSSHXA4fmD1mfsWX+QkxESXTxwYmMRgV51XWRNYdwewajzsJXBDbaFKZ5dILVn6cGBf2BDhHfLzkL0zXBJ5lURKzqhHQCPr3iZfKRBWl', '::1', 'Mozilla/5.0 (iPhone; CPU iPhone OS 13_2_3 like Mac OS X) AppleWebKit/605.1.15 (KHTML, like Gecko) Version/13.0.3 Mobile/15E148 Safari/604.1', '2021-03-27 11:49:00', '1');
INSERT INTO `db_token` VALUES ('2958', '1', 'admin', 'DSpazZTPAw74/cO3CGcMWCQXt+sVyov5PHBfsOl5DnnPbWvgK7aaG5dj07/2+f7Gsy4QC9fbnf3XJhDS3GLouuE6VzLc/YMx9+OD6Df44mkCGxtOTYectggi9Pzn+VRar3IvGIE0jCalDGDi7YrLm8PrNSSHXA4fmD1mfsWX+QkxESXTxwYmMRgV51XWRNYdwewajzsJXBDbaFKZ5dILVn6cGBf2BDhHfLzkL0zXBJ5lURKzqhHQCPr3iZfKRBWl', '::1', 'Mozilla/5.0 (iPhone; CPU iPhone OS 13_2_3 like Mac OS X) AppleWebKit/605.1.15 (KHTML, like Gecko) Version/13.0.3 Mobile/15E148 Safari/604.1', '2021-03-27 11:53:24', '1');
INSERT INTO `db_token` VALUES ('2959', '1', 'admin', 'DSpazZTPAw74/cO3CGcMWHl1dS07H2lCWkRAs7FkWjWn7bbRMt9CkNAUT5uC+AnUHAcLh65Pzj8HpcEippTRrroeiFWBhryEw02lQ5sMNlafnKXJSodu6KOWK5oE81l4SE4bN1kNJVXJJ9mREn8tN/lAyRcl5pI4/RGROzV6XdHPlnfDcxZIlYlGvPVur3OHXmVTwTRWVoudA9mTmsmauY3Q224bWhz0NSXsc2QVJEqeczyO12YlF0o6ytAs/K0h', '180.140.160.167', 'Mozilla/5.0 (iPhone; CPU iPhone OS 14_1 like Mac OS X) AppleWebKit/605.1.15 (KHTML, like Gecko) Version/14.0 Mobile/15E148 Safari/604.1', '2021-03-27 11:56:41', '1');
INSERT INTO `db_token` VALUES ('2960', '1', 'admin', 'DSpazZTPAw74/cO3CGcMWCQXt+sVyov5PHBfsOl5DnnPbWvgK7aaG5dj07/2+f7GjMrHHUqbk80ynbLjDR3mMZNg/Po0tpNtWYQgOpQQXE7MN9C8XSCNpHzmmjgmsVK6D1w8OJ5gIi9eaLx1ouXHVFsfnRh+ICk6aEFZ63vkjrOP2CGpmML41eWAyRzGI+U4ctCwJbv+y0AZOx+VMLC+VQ==', '::1', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/89.0.4389.90 Safari/537.36', '2021-03-27 12:00:25', '1');
INSERT INTO `db_token` VALUES ('2962', '1', 'admin', 'DSpazZTPAw74/cO3CGcMWHl1dS07H2lCWkRAs7FkWjWn7bbRMt9CkNAUT5uC+AnUCnZ87m0DlLkHxDDBUQ5zQDGC4QunO76cObJm4MewneMCtTJ3yRrd8TnyI9rTaWU8fWu9rKRppzqbM9nWkB3gXhDK/SysMzC14raYxTfcaqrDJUDtcwYA2KxEY4RJIDFhbpK8sl9fZr0SBA/RJyX0gGJU+1U09HtxLzYTH8SpeQE=', '180.140.160.167', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/89.0.4389.90 Safari/537.36', '2021-03-29 14:38:38', '1');
INSERT INTO `db_token` VALUES ('2964', '1', 'admin', 'DSpazZTPAw74/cO3CGcMWLB2EB/3rQ5xzMwD6Wh7RCPE2wcGpDFo/ZqYzx+5KYmBeVJfFIctEc5WpPadGkFHQUmHw3CPXNu5hLJL+Z02v8nenfOsILcB2GNNcNBW5tgrZfsnryf3sHFxFbwP1fjC/thGvwBW0IoAR09HmnwpUYxDxD7o4BQL/rmdMvHKnQsrhzDJW1i3l/V+PS4Jsxs6QMw8jJcZNpO/DYd6sVyaofs=', '116.252.94.158', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/89.0.4389.90 Safari/537.36', '2021-03-30 15:56:14', '1');
INSERT INTO `db_token` VALUES ('2965', '1', 'admin', 'DSpazZTPAw74/cO3CGcMWLB2EB/3rQ5xzMwD6Wh7RCOg6xHPMcib6ejv2bpBVzzBeVJfFIctEc5WpPadGkFHQUmHw3CPXNu5hLJL+Z02v8nenfOsILcB2GNNcNBW5tgrZfsnryf3sHFxFbwP1fjC/thGvwBW0IoAR09HmnwpUYxDxD7o4BQL/rmdMvHKnQsrc2D9bP7HmFmZfZpW7SujYGJU+1U09HtxLzYTH8SpeQE=', '116.252.95.214', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/89.0.4389.114 Safari/537.36', '2021-04-10 11:16:05', '1');
INSERT INTO `db_token` VALUES ('2969', '1', 'admin', 'DSpazZTPAw74/cO3CGcMWCQXt+sVyov5PHBfsOl5DnnPbWvgK7aaG5dj07/2+f7GjMrHHUqbk80ynbLjDR3mMZNg/Po0tpNtWYQgOpQQXE7MN9C8XSCNpHzmmjgmsVK6D1w8OJ5gIi9eaLx1ouXHVL+0iZqPW4eWXdCOIWyNWQWnGeUcA1SSkCyqeMpzCXIQctCwJbv+y0AZOx+VMLC+VQ==', '::1', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/90.0.4430.85 Safari/537.36', '2021-04-25 14:36:21', '1');
INSERT INTO `db_token` VALUES ('2970', '1', 'admin', 'DSpazZTPAw74/cO3CGcMWI1XSrMoX/mbSkVxRLVy/nCQv1pRmkN4od4yQNM3k7rwCnZ87m0DlLkHxDDBUQ5zQDGC4QunO76cObJm4MewneMCtTJ3yRrd8TnyI9rTaWU8fWu9rKRppzqbM9nWkB3gXhDK/SysMzC14raYxTfcaqqgN6kmtuRJDx3jHkxWMDWd8d1kHcn6S9SrLLnqjnQR/GJU+1U09HtxLzYTH8SpeQE=', '171.104.223.239', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/90.0.4430.85 Safari/537.36', '2021-04-26 11:45:14', '1');
INSERT INTO `db_token` VALUES ('2971', '1', 'admin', 'DSpazZTPAw74/cO3CGcMWHl1dS07H2lCWkRAs7FkWjX1PVPJfPl5uL0KT4lX3FoBCnZ87m0DlLkHxDDBUQ5zQDGC4QunO76cObJm4MewneMCtTJ3yRrd8TnyI9rTaWU8fWu9rKRppzqbM9nWkB3gXhDK/SysMzC14raYxTfcaqqgN6kmtuRJDx3jHkxWMDWd8d1kHcn6S9SrLLnqjnQR/KDSqDQGedmYnPEC+Efj8l1iVPtVNPR7cS82Ex/EqXkB', '180.140.161.185', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/90.0.4430.85 Safari/537.36 Edg/90.0.818.46', '2021-04-27 12:08:41', '1');
INSERT INTO `db_token` VALUES ('2973', '1', 'admin', 'DSpazZTPAw74/cO3CGcMWHl1dS07H2lCWkRAs7FkWjX1PVPJfPl5uL0KT4lX3FoBCnZ87m0DlLkHxDDBUQ5zQDGC4QunO76cObJm4MewneMCtTJ3yRrd8TnyI9rTaWU8fWu9rKRppzqbM9nWkB3gXhDK/SysMzC14raYxTfcaqqgN6kmtuRJDx3jHkxWMDWd8d1kHcn6S9SrLLnqjnQR/GJU+1U09HtxLzYTH8SpeQE=', '180.140.161.185', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/90.0.4430.85 Safari/537.36', '2021-04-28 09:51:20', '1');
INSERT INTO `db_token` VALUES ('2975', '1', 'admin', 'DSpazZTPAw74/cO3CGcMWCQXt+sVyov5PHBfsOl5DnnPbWvgK7aaG5dj07/2+f7GjMrHHUqbk80ynbLjDR3mMZNg/Po0tpNtWYQgOpQQXE7MN9C8XSCNpHzmmjgmsVK6D1w8OJ5gIi9eaLx1ouXHVL+0iZqPW4eWXdCOIWyNWQWnGeUcA1SSkCyqeMpzCXIQctCwJbv+y0AZOx+VMLC+VQ==', '::1', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/90.0.4430.85 Safari/537.36', '2021-04-28 10:51:09', '1');
INSERT INTO `db_token` VALUES ('2978', '1', 'admin', 'DSpazZTPAw74/cO3CGcMWHl1dS07H2lCWkRAs7FkWjX1PVPJfPl5uL0KT4lX3FoBCnZ87m0DlLkHxDDBUQ5zQDGC4QunO76cObJm4MewneMCtTJ3yRrd8TnyI9rTaWU8fWu9rKRppzqbM9nWkB3gXhDK/SysMzC14raYxTfcaqqgN6kmtuRJDx3jHkxWMDWd8d1kHcn6S9SrLLnqjnQR/GJU+1U09HtxLzYTH8SpeQE=', '180.140.161.185', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/90.0.4430.85 Safari/537.36', '2021-04-28 16:08:36', '1');
INSERT INTO `db_token` VALUES ('2979', '1', 'admin', 'DSpazZTPAw74/cO3CGcMWHl1dS07H2lCWkRAs7FkWjX1PVPJfPl5uL0KT4lX3FoBCnZ87m0DlLkHxDDBUQ5zQDGC4QunO76cObJm4MewneMCtTJ3yRrd8TnyI9rTaWU8fWu9rKRppzqbM9nWkB3gXhDK/SysMzC14raYxTfcaqqgN6kmtuRJDx3jHkxWMDWd8d1kHcn6S9SrLLnqjnQR/GJU+1U09HtxLzYTH8SpeQE=', '180.140.161.185', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/90.0.4430.85 Safari/537.36', '2021-04-28 16:14:42', '1');
INSERT INTO `db_token` VALUES ('2981', '1', 'admin', 'DSpazZTPAw74/cO3CGcMWEhFUh7y9mbAsKSi8FW6bXXv8fZf2igki1ONfg/PgWiRajLfPu7J48lIiBVT/33qCG74BGKZu0O2e1xTlHxdtqdP+m9MBe0dlJcEgkQvxKQTaYfl+PeQIrAXNjFIKw7VWhGtIU6CUf/hxfngO9ateyFeTpySJGvsekZRcaOh5VnKzICZzCWnyeU4vsE5KX5EdHoHVXwKi9vx/OcCrU8qnNFXjTV8nezM2gEcqcx07cap', '180.139.209.6', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/90.0.4430.85 Safari/537.36 Edg/90.0.818.46', '2021-04-28 21:04:31', '1');
INSERT INTO `db_token` VALUES ('2982', '1', 'admin', 'DSpazZTPAw74/cO3CGcMWHl1dS07H2lCWkRAs7FkWjX1PVPJfPl5uL0KT4lX3FoBbqBdvdIQwx2jQGmrZRPKwwNVcn3s449IUl3T8vO2o7nAFTKdtzBgaitwnBFmZ5VDDlPw8eLsk4kbJmR6vOKY7y0aiON3De31+u4C4E4fQDU7+oroFOcUZL2Dy2Zz5WYZSfAtZOh7MCIJTCJtlEUXa+WCvanwwnpCzaenOdZdFjDfc0ylN9l+rmCPrC1J7ziS', '180.140.161.185', 'Mozilla/5.0 (Linux; Android 6.0; Nexus 5 Build/MRA58N) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/76.0.3809.132 Mobile Safari/537.36', '2021-04-29 09:49:57', '1');
INSERT INTO `db_token` VALUES ('2983', '1', 'admin', 'DSpazZTPAw74/cO3CGcMWHl1dS07H2lCWkRAs7FkWjX1PVPJfPl5uL0KT4lX3FoBCnZ87m0DlLkHxDDBUQ5zQDGC4QunO76cObJm4MewneMCtTJ3yRrd8TnyI9rTaWU8fWu9rKRppzqbM9nWkB3gXhDK/SysMzC14raYxTfcaqqgN6kmtuRJDx3jHkxWMDWd8d1kHcn6S9SrLLnqjnQR/GJU+1U09HtxLzYTH8SpeQE=', '180.140.161.185', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/90.0.4430.85 Safari/537.36', '2021-04-29 10:16:08', '1');
INSERT INTO `db_token` VALUES ('2984', '1', 'admin', 'DSpazZTPAw74/cO3CGcMWHl1dS07H2lCWkRAs7FkWjX1PVPJfPl5uL0KT4lX3FoBCnZ87m0DlLkHxDDBUQ5zQDGC4QunO76cObJm4MewneMCtTJ3yRrd8TnyI9rTaWU8fWu9rKRppzqbM9nWkB3gXhDK/SysMzC14raYxTfcaqrDJUDtcwYA2KxEY4RJIDFhyTrppXaACfr156Jmt2ezdptGI1B0hxOKO5cdkekZcew=', '180.140.161.185', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/89.0.4389.128 Safari/537.36', '2021-04-29 10:27:22', '1');
INSERT INTO `db_token` VALUES ('2986', '1', 'admin', 'DSpazZTPAw74/cO3CGcMWCQXt+sVyov5PHBfsOl5DnnPbWvgK7aaG5dj07/2+f7GN2L7EVhH01Y42537BgYHv081kzMrjbEzrNM1iLIEfH2thY9m4AbI25oKmBZcmZdECdk+sJfrMQYpnheXJT21gy9fMKn1cXnHE4+Q7jHnQOGyUT2vPw4zmCFrkcqbTgQrg5uzBb1iPTXRdptaYibdOQwbZKti53hx0mFosS62DHw=', '::1', 'Mozilla/5.0 (Linux; Android 6.0; Nexus 5 Build/MRA58N) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/76.0.3809.132 Mobile Safari/537.36', '2021-04-29 11:02:50', '1');
INSERT INTO `db_token` VALUES ('2987', '1', 'admin', 'DSpazZTPAw74/cO3CGcMWHl1dS07H2lCWkRAs7FkWjX1PVPJfPl5uL0KT4lX3FoBbqBdvdIQwx2jQGmrZRPKwwNVcn3s449IUl3T8vO2o7nAFTKdtzBgaitwnBFmZ5VDDlPw8eLsk4kbJmR6vOKY7y0aiON3De31+u4C4E4fQDU7+oroFOcUZL2Dy2Zz5WYZSfAtZOh7MCIJTCJtlEUXa+WCvanwwnpCzaenOdZdFjDfc0ylN9l+rmCPrC1J7ziS', '180.140.161.185', 'Mozilla/5.0 (Linux; Android 6.0; Nexus 5 Build/MRA58N) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/76.0.3809.132 Mobile Safari/537.36', '2021-04-29 11:36:24', '1');
INSERT INTO `db_token` VALUES ('2988', '1', 'admin', 'DSpazZTPAw74/cO3CGcMWHl1dS07H2lCWkRAs7FkWjX1PVPJfPl5uL0KT4lX3FoBCnZ87m0DlLkHxDDBUQ5zQDGC4QunO76cObJm4MewneMCtTJ3yRrd8TnyI9rTaWU8fWu9rKRppzqbM9nWkB3gXhDK/SysMzC14raYxTfcaqpkURW7lnKB1RrEE01rAjk+3yWNTPXRvkx5WGLgLdQtp5tGI1B0hxOKO5cdkekZcew=', '180.140.161.185', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/76.0.3809.132 Safari/537.36', '2021-04-29 15:26:20', '1');
INSERT INTO `db_token` VALUES ('2989', '1', 'admin', 'DSpazZTPAw74/cO3CGcMWHl1dS07H2lCWkRAs7FkWjX1PVPJfPl5uL0KT4lX3FoBbqBdvdIQwx2jQGmrZRPKwwNVcn3s449IUl3T8vO2o7nAFTKdtzBgaitwnBFmZ5VDDlPw8eLsk4kbJmR6vOKY7y0aiON3De31+u4C4E4fQDU7+oroFOcUZL2Dy2Zz5WYZSfAtZOh7MCIJTCJtlEUXa+WCvanwwnpCzaenOdZdFjDfc0ylN9l+rmCPrC1J7ziS', '180.140.161.185', 'Mozilla/5.0 (Linux; Android 6.0; Nexus 5 Build/MRA58N) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/76.0.3809.132 Mobile Safari/537.36', '2021-04-29 15:27:12', '1');
INSERT INTO `db_token` VALUES ('2991', '1', 'admin', 'DSpazZTPAw74/cO3CGcMWHl1dS07H2lCWkRAs7FkWjVo+aszxypVmbfLaF70fzdgeVJfFIctEc5WpPadGkFHQUmHw3CPXNu5hLJL+Z02v8nenfOsILcB2GNNcNBW5tgrZfsnryf3sHFxFbwP1fjC/thGvwBW0IoAR09HmnwpUYxDxD7o4BQL/rmdMvHKnQsr5b/i99WOvnB6TLtAoaGsQcw8jJcZNpO/DYd6sVyaofs=', '180.140.163.18', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/89.0.4389.82 Safari/537.36', '2021-05-13 16:24:14', '1');
INSERT INTO `db_token` VALUES ('2994', '1', 'admin', 'DSpazZTPAw74/cO3CGcMWHl1dS07H2lCWkRAs7FkWjVo+aszxypVmbfLaF70fzdgeVJfFIctEc5WpPadGkFHQUmHw3CPXNu5hLJL+Z02v8nenfOsILcB2GNNcNBW5tgrZfsnryf3sHFxFbwP1fjC/thGvwBW0IoAR09HmnwpUYw/gviwlAwBdX6p21p9FB29Z8j8idraqnrQC+g04DZ04cw8jJcZNpO/DYd6sVyaofs=', '180.140.163.18', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/90.0.4430.93 Safari/537.36', '2021-05-14 16:10:13', '1');
INSERT INTO `db_token` VALUES ('2996', '1', 'admin', 'DSpazZTPAw74/cO3CGcMWHl1dS07H2lCWkRAs7FkWjVo+aszxypVmbfLaF70fzdgeVJfFIctEc5WpPadGkFHQUmHw3CPXNu5hLJL+Z02v8nenfOsILcB2GNNcNBW5tgrZfsnryf3sHFxFbwP1fjC/thGvwBW0IoAR09HmnwpUYw/gviwlAwBdX6p21p9FB29ddLrJrdo8jFX1EylG/ps0GJU+1U09HtxLzYTH8SpeQE=', '180.140.163.18', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/90.0.4430.212 Safari/537.36', '2021-05-16 15:18:16', '1');
INSERT INTO `db_token` VALUES ('3001', '1', 'admin', 'DSpazZTPAw74/cO3CGcMWLB2EB/3rQ5xzMwD6Wh7RCPXBYjAumYS8zVqNPJpvFGpCnZ87m0DlLkHxDDBUQ5zQDGC4QunO76cObJm4MewneMCtTJ3yRrd8TnyI9rTaWU8fWu9rKRppzqbM9nWkB3gXhDK/SysMzC14raYxTfcaqqgN6kmtuRJDx3jHkxWMDWd3pV23cHQk4zDJjNxJQt24ptGI1B0hxOKO5cdkekZcew=', '116.252.135.186', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/90.0.4430.212 Safari/537.36', '2021-05-28 11:18:11', '1');
INSERT INTO `db_token` VALUES ('3002', '1', 'admin', 'DSpazZTPAw74/cO3CGcMWLB2EB/3rQ5xzMwD6Wh7RCPXBYjAumYS8zVqNPJpvFGpCnZ87m0DlLkHxDDBUQ5zQDGC4QunO76cObJm4MewneMCtTJ3yRrd8TnyI9rTaWU8fWu9rKRppzqbM9nWkB3gXhDK/SysMzC14raYxTfcaqqgN6kmtuRJDx3jHkxWMDWd3pV23cHQk4zDJjNxJQt24mVqNPRLAhyJWECY6o4RHt17bANlSUiIAsGq16XRDcKM', '116.252.135.186', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/90.0.4430.212 Safari/537.36 Edg/90.0.818.66', '2021-05-28 14:28:25', '1');
INSERT INTO `db_token` VALUES ('3003', '1', 'admin', 'DSpazZTPAw74/cO3CGcMWLB2EB/3rQ5xzMwD6Wh7RCPXBYjAumYS8zVqNPJpvFGpCnZ87m0DlLkHxDDBUQ5zQDGC4QunO76cObJm4MewneMCtTJ3yRrd8TnyI9rTaWU8fWu9rKRppzqbM9nWkB3gXhDK/SysMzC14raYxTfcaqqgN6kmtuRJDx3jHkxWMDWd3pV23cHQk4zDJjNxJQt24ptGI1B0hxOKO5cdkekZcew=', '116.252.135.186', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/90.0.4430.212 Safari/537.36', '2021-05-31 16:36:17', '1');
INSERT INTO `db_token` VALUES ('3072', '1', 'admin', 'DSpazZTPAw74/cO3CGcMWOe6s2TFv/uP93EC8N0msfXPcWMbX4dxQ87IRdf4gUqMeVJfFIctEc5WpPadGkFHQUmHw3CPXNu5hLJL+Z02v8nenfOsILcB2GNNcNBW5tgrZfsnryf3sHFxFbwP1fjC/thGvwBW0IoAR09HmnwpUYw+Am9C0O65mJ3IG8OON/VcWlaAkj+qrJojvrA7nYeHwMw8jJcZNpO/DYd6sVyaofs=', '222.216.43.102', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/91.0.4472.77 Safari/537.36', '2021-06-10 11:28:47', '1');
INSERT INTO `db_token` VALUES ('3075', '1', 'admin', 'DSpazZTPAw74/cO3CGcMWOe6s2TFv/uP93EC8N0msfXPcWMbX4dxQ87IRdf4gUqMeVJfFIctEc5WpPadGkFHQUmHw3CPXNu5hLJL+Z02v8nenfOsILcB2GNNcNBW5tgrZfsnryf3sHFxFbwP1fjC/thGvwBW0IoAR09HmnwpUYygSbW9svzLGp7a4/kFA5FcGOGygJm6sAiwUc18lvy88GJU+1U09HtxLzYTH8SpeQE=', '222.216.43.102', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/76.0.3809.132 Safari/537.36', '2021-06-13 15:18:36', '1');
INSERT INTO `db_token` VALUES ('3076', '1', 'admin', 'PP7OQ8wX9vwFv2qOn+mWj31hd8QXL/XZCPMfvbroqj5RAj/eG5TNlM6pfygtfamnuzf5F/UsnEsPzuvHxYZqTHpsGlHDj61dfdXsmgO6DNKkjw3BmUC1ICkL7SesAY7Q8Kd+QlL1QCUoLnYlZAUrPUfx1V3rePUv6uYJBiFwdSOfN30hBaH5fvOg6jX7jbANFMRwzl+JPk+ArHf7SValohePCVOQsyQtRjEAsL1ujy8=', '222.216.43.17', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/91.0.4472.106 Safari/537.36', '2021-06-24 14:29:12', '1');
INSERT INTO `db_token` VALUES ('3078', '1', 'admin', 'PP7OQ8wX9vwFv2qOn+mWj31hd8QXL/XZCPMfvbroqj5RAj/eG5TNlM6pfygtfamnuzf5F/UsnEsPzuvHxYZqTHpsGlHDj61dfdXsmgO6DNKkjw3BmUC1ICkL7SesAY7Q8Kd+QlL1QCUoLnYlZAUrPUfx1V3rePUv6uYJBiFwdSOfN30hBaH5fvOg6jX7jbANkUnLMSmgooOnhchPsPK1xhePCVOQsyQtRjEAsL1ujy8=', '222.216.43.17', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/91.0.4472.114 Safari/537.36', '2021-06-25 14:50:09', '1');
INSERT INTO `db_token` VALUES ('3079', '1', 'admin', 'PP7OQ8wX9vwFv2qOn+mWj31hd8QXL/XZCPMfvbroqj5RAj/eG5TNlM6pfygtfamnuzf5F/UsnEsPzuvHxYZqTHpsGlHDj61dfdXsmgO6DNKkjw3BmUC1ICkL7SesAY7Q8Kd+QlL1QCUoLnYlZAUrPUfx1V3rePUv6uYJBiFwdSOfN30hBaH5fvOg6jX7jbANkUnLMSmgooOnhchPsPK1xhePCVOQsyQtRjEAsL1ujy8=', '222.216.43.17', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/91.0.4472.114 Safari/537.36', '2021-06-26 09:34:06', '1');
INSERT INTO `db_token` VALUES ('3081', '1', 'admin', 'PP7OQ8wX9vwFv2qOn+mWj31hd8QXL/XZCPMfvbroqj5RAj/eG5TNlM6pfygtfamnuzf5F/UsnEsPzuvHxYZqTHpsGlHDj61dfdXsmgO6DNKkjw3BmUC1ICkL7SesAY7Q8Kd+QlL1QCUoLnYlZAUrPUfx1V3rePUv6uYJBiFwdSOfN30hBaH5fvOg6jX7jbANkUnLMSmgooOnhchPsPK1xhePCVOQsyQtRjEAsL1ujy8=', '222.216.43.17', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/91.0.4472.114 Safari/537.36', '2021-06-27 09:44:09', '1');
INSERT INTO `db_token` VALUES ('3082', '1', 'admin', 'PP7OQ8wX9vwFv2qOn+mWj31hd8QXL/XZCPMfvbroqj5RAj/eG5TNlM6pfygtfamnuzf5F/UsnEsPzuvHxYZqTHpsGlHDj61dfdXsmgO6DNKkjw3BmUC1ICkL7SesAY7Q8Kd+QlL1QCUoLnYlZAUrPUfx1V3rePUv6uYJBiFwdSOfN30hBaH5fvOg6jX7jbANkUnLMSmgooOnhchPsPK1xhePCVOQsyQtRjEAsL1ujy8=', '222.216.43.17', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/91.0.4472.114 Safari/537.36', '2021-06-27 09:44:15', '1');
INSERT INTO `db_token` VALUES ('3083', '1', 'admin', 'PP7OQ8wX9vwFv2qOn+mWj31hd8QXL/XZCPMfvbroqj5RAj/eG5TNlM6pfygtfamnuzf5F/UsnEsPzuvHxYZqTHpsGlHDj61dfdXsmgO6DNKkjw3BmUC1ICkL7SesAY7Q8Kd+QlL1QCUoLnYlZAUrPUfx1V3rePUv6uYJBiFwdSOfN30hBaH5fvOg6jX7jbANkUnLMSmgooOnhchPsPK1xhePCVOQsyQtRjEAsL1ujy8=', '222.216.43.17', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/91.0.4472.114 Safari/537.36', '2021-06-27 09:45:06', '1');
INSERT INTO `db_token` VALUES ('3084', '1', 'admin', 'PP7OQ8wX9vwFv2qOn+mWj31hd8QXL/XZCPMfvbroqj5RAj/eG5TNlM6pfygtfamnuzf5F/UsnEsPzuvHxYZqTHpsGlHDj61dfdXsmgO6DNKkjw3BmUC1ICkL7SesAY7Q8Kd+QlL1QCUoLnYlZAUrPUfx1V3rePUv6uYJBiFwdSOfN30hBaH5fvOg6jX7jbANkUnLMSmgooOnhchPsPK1xhePCVOQsyQtRjEAsL1ujy8=', '222.216.43.17', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/91.0.4472.114 Safari/537.36', '2021-06-27 09:45:16', '1');
INSERT INTO `db_token` VALUES ('3085', '1', 'admin', 'PP7OQ8wX9vwFv2qOn+mWj31hd8QXL/XZCPMfvbroqj5RAj/eG5TNlM6pfygtfamnuzf5F/UsnEsPzuvHxYZqTHpsGlHDj61dfdXsmgO6DNKkjw3BmUC1ICkL7SesAY7Q8Kd+QlL1QCUoLnYlZAUrPUfx1V3rePUv6uYJBiFwdSOfN30hBaH5fvOg6jX7jbANkUnLMSmgooOnhchPsPK1xhePCVOQsyQtRjEAsL1ujy8=', '222.216.43.17', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/91.0.4472.114 Safari/537.36', '2021-06-27 09:45:17', '1');
INSERT INTO `db_token` VALUES ('3086', '1', 'admin', 'PP7OQ8wX9vwFv2qOn+mWj31hd8QXL/XZCPMfvbroqj5RAj/eG5TNlM6pfygtfamnuzf5F/UsnEsPzuvHxYZqTHpsGlHDj61dfdXsmgO6DNKkjw3BmUC1ICkL7SesAY7Q8Kd+QlL1QCUoLnYlZAUrPUfx1V3rePUv6uYJBiFwdSOfN30hBaH5fvOg6jX7jbANkUnLMSmgooOnhchPsPK1xhePCVOQsyQtRjEAsL1ujy8=', '222.216.43.17', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/91.0.4472.114 Safari/537.36', '2021-06-27 09:49:45', '1');
INSERT INTO `db_token` VALUES ('3087', '1', 'admin', 'PP7OQ8wX9vwFv2qOn+mWj31hd8QXL/XZCPMfvbroqj5RAj/eG5TNlM6pfygtfamnuzf5F/UsnEsPzuvHxYZqTHpsGlHDj61dfdXsmgO6DNKkjw3BmUC1ICkL7SesAY7Q8Kd+QlL1QCUoLnYlZAUrPUfx1V3rePUv6uYJBiFwdSOfN30hBaH5fvOg6jX7jbANkUnLMSmgooOnhchPsPK1xhePCVOQsyQtRjEAsL1ujy8=', '222.216.43.17', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/91.0.4472.114 Safari/537.36', '2021-06-27 10:14:55', '1');
INSERT INTO `db_token` VALUES ('3088', '1', 'admin', 'PP7OQ8wX9vwFv2qOn+mWj31hd8QXL/XZCPMfvbroqj5RAj/eG5TNlM6pfygtfamnuzf5F/UsnEsPzuvHxYZqTHpsGlHDj61dfdXsmgO6DNKkjw3BmUC1ICkL7SesAY7Q8Kd+QlL1QCUoLnYlZAUrPUfx1V3rePUv6uYJBiFwdSOfN30hBaH5fvOg6jX7jbANkUnLMSmgooOnhchPsPK1xhePCVOQsyQtRjEAsL1ujy8=', '222.216.43.17', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/91.0.4472.114 Safari/537.36', '2021-06-27 10:20:54', '1');
INSERT INTO `db_token` VALUES ('3089', '1', 'admin', 'PP7OQ8wX9vwFv2qOn+mWj31hd8QXL/XZCPMfvbroqj5RAj/eG5TNlM6pfygtfamnuzf5F/UsnEsPzuvHxYZqTHpsGlHDj61dfdXsmgO6DNKkjw3BmUC1ICkL7SesAY7Q8Kd+QlL1QCUoLnYlZAUrPUfx1V3rePUv6uYJBiFwdSOfN30hBaH5fvOg6jX7jbANkUnLMSmgooOnhchPsPK1xhePCVOQsyQtRjEAsL1ujy8=', '222.216.43.17', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/91.0.4472.114 Safari/537.36', '2021-06-27 10:20:56', '1');
INSERT INTO `db_token` VALUES ('3090', '1', 'admin', 'PP7OQ8wX9vwFv2qOn+mWj31hd8QXL/XZCPMfvbroqj5RAj/eG5TNlM6pfygtfamnuzf5F/UsnEsPzuvHxYZqTHpsGlHDj61dfdXsmgO6DNKkjw3BmUC1ICkL7SesAY7Q8Kd+QlL1QCUoLnYlZAUrPUfx1V3rePUv6uYJBiFwdSOfN30hBaH5fvOg6jX7jbANkUnLMSmgooOnhchPsPK1xhePCVOQsyQtRjEAsL1ujy8=', '222.216.43.17', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/91.0.4472.114 Safari/537.36', '2021-06-27 10:21:14', '1');
INSERT INTO `db_token` VALUES ('3091', '1', 'admin', 'PP7OQ8wX9vwFv2qOn+mWj31hd8QXL/XZCPMfvbroqj5RAj/eG5TNlM6pfygtfamnuzf5F/UsnEsPzuvHxYZqTHpsGlHDj61dfdXsmgO6DNKkjw3BmUC1ICkL7SesAY7Q8Kd+QlL1QCUoLnYlZAUrPUfx1V3rePUv6uYJBiFwdSOfN30hBaH5fvOg6jX7jbANkUnLMSmgooOnhchPsPK1xhePCVOQsyQtRjEAsL1ujy8=', '222.216.43.17', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/91.0.4472.114 Safari/537.36', '2021-06-27 10:21:27', '1');
INSERT INTO `db_token` VALUES ('3092', '1', 'admin', 'PP7OQ8wX9vwFv2qOn+mWj31hd8QXL/XZCPMfvbroqj5RAj/eG5TNlM6pfygtfamnuzf5F/UsnEsPzuvHxYZqTHpsGlHDj61dfdXsmgO6DNKkjw3BmUC1ICkL7SesAY7Q8Kd+QlL1QCUoLnYlZAUrPUfx1V3rePUv6uYJBiFwdSOfN30hBaH5fvOg6jX7jbANkUnLMSmgooOnhchPsPK1xhePCVOQsyQtRjEAsL1ujy8=', '222.216.43.17', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/91.0.4472.114 Safari/537.36', '2021-06-27 10:51:01', '1');
INSERT INTO `db_token` VALUES ('3093', '1', 'admin', 'PP7OQ8wX9vwFv2qOn+mWj31hd8QXL/XZCPMfvbroqj5RAj/eG5TNlM6pfygtfamnuzf5F/UsnEsPzuvHxYZqTHpsGlHDj61dfdXsmgO6DNKkjw3BmUC1ICkL7SesAY7Q8Kd+QlL1QCUoLnYlZAUrPUfx1V3rePUv6uYJBiFwdSOfN30hBaH5fvOg6jX7jbANkUnLMSmgooOnhchPsPK1xhePCVOQsyQtRjEAsL1ujy8=', '222.216.43.17', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/91.0.4472.114 Safari/537.36', '2021-06-27 10:51:08', '1');
INSERT INTO `db_token` VALUES ('3094', '1', 'admin', 'PP7OQ8wX9vwFv2qOn+mWj31hd8QXL/XZCPMfvbroqj5RAj/eG5TNlM6pfygtfamnuzf5F/UsnEsPzuvHxYZqTHpsGlHDj61dfdXsmgO6DNKkjw3BmUC1ICkL7SesAY7Q8Kd+QlL1QCUoLnYlZAUrPUfx1V3rePUv6uYJBiFwdSOfN30hBaH5fvOg6jX7jbANkUnLMSmgooOnhchPsPK1xhePCVOQsyQtRjEAsL1ujy8=', '222.216.43.17', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/91.0.4472.114 Safari/537.36', '2021-06-27 10:51:49', '1');
INSERT INTO `db_token` VALUES ('3095', '1', 'admin', 'PP7OQ8wX9vwFv2qOn+mWj31hd8QXL/XZCPMfvbroqj5RAj/eG5TNlM6pfygtfamnuzf5F/UsnEsPzuvHxYZqTHpsGlHDj61dfdXsmgO6DNKkjw3BmUC1ICkL7SesAY7Q8Kd+QlL1QCUoLnYlZAUrPUfx1V3rePUv6uYJBiFwdSOfN30hBaH5fvOg6jX7jbANkUnLMSmgooOnhchPsPK1xhePCVOQsyQtRjEAsL1ujy8=', '222.216.43.17', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/91.0.4472.114 Safari/537.36', '2021-06-27 10:52:13', '1');
INSERT INTO `db_token` VALUES ('3096', '1', 'admin', 'PP7OQ8wX9vwFv2qOn+mWj31hd8QXL/XZCPMfvbroqj5RAj/eG5TNlM6pfygtfamnuzf5F/UsnEsPzuvHxYZqTHpsGlHDj61dfdXsmgO6DNKkjw3BmUC1ICkL7SesAY7Q8Kd+QlL1QCUoLnYlZAUrPUfx1V3rePUv6uYJBiFwdSOfN30hBaH5fvOg6jX7jbANkUnLMSmgooOnhchPsPK1xhePCVOQsyQtRjEAsL1ujy8=', '222.216.43.17', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/91.0.4472.114 Safari/537.36', '2021-06-27 10:53:09', '1');
INSERT INTO `db_token` VALUES ('3097', '1', 'admin', 'PP7OQ8wX9vwFv2qOn+mWj31hd8QXL/XZCPMfvbroqj5RAj/eG5TNlM6pfygtfamnuzf5F/UsnEsPzuvHxYZqTHpsGlHDj61dfdXsmgO6DNKkjw3BmUC1ICkL7SesAY7Q8Kd+QlL1QCUoLnYlZAUrPUfx1V3rePUv6uYJBiFwdSOfN30hBaH5fvOg6jX7jbANkUnLMSmgooOnhchPsPK1xhePCVOQsyQtRjEAsL1ujy8=', '222.216.43.17', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/91.0.4472.114 Safari/537.36', '2021-06-27 10:53:13', '1');
INSERT INTO `db_token` VALUES ('3098', '1', 'admin', 'PP7OQ8wX9vwFv2qOn+mWj31hd8QXL/XZCPMfvbroqj5RAj/eG5TNlM6pfygtfamnuzf5F/UsnEsPzuvHxYZqTHpsGlHDj61dfdXsmgO6DNKkjw3BmUC1ICkL7SesAY7Q8Kd+QlL1QCUoLnYlZAUrPUfx1V3rePUv6uYJBiFwdSOfN30hBaH5fvOg6jX7jbANkUnLMSmgooOnhchPsPK1xhePCVOQsyQtRjEAsL1ujy8=', '222.216.43.17', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/91.0.4472.114 Safari/537.36', '2021-06-27 10:54:03', '1');
INSERT INTO `db_token` VALUES ('3099', '1', 'admin', 'PP7OQ8wX9vwFv2qOn+mWj31hd8QXL/XZCPMfvbroqj5RAj/eG5TNlM6pfygtfamnuzf5F/UsnEsPzuvHxYZqTHpsGlHDj61dfdXsmgO6DNKkjw3BmUC1ICkL7SesAY7Q8Kd+QlL1QCUoLnYlZAUrPUfx1V3rePUv6uYJBiFwdSOfN30hBaH5fvOg6jX7jbANkUnLMSmgooOnhchPsPK1xhePCVOQsyQtRjEAsL1ujy8=', '222.216.43.17', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/91.0.4472.114 Safari/537.36', '2021-06-27 10:55:01', '1');
INSERT INTO `db_token` VALUES ('3100', '1', 'admin', 'PP7OQ8wX9vwFv2qOn+mWj31hd8QXL/XZCPMfvbroqj6TrT9uJGYC/jtSxTMrcS7xPfbJKlYUc3wIUyiOrusdNb8xNsL4r3JmGtOLDjP5f5xX25i+b8YFYbFRcDi0PLMLq4/H0sQura3TCbzvqJKW2Z6utUUojZWdYz9Exk/P4DBZiX2/gX3Gq3hTDIi/LXTVZJHvUWuXsepF8mA+1eFr7AtdFA3SUxk98kNk0Fm/N1hXjTV8nezM2gEcqcx07cap', '222.216.163.172', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/91.0.4472.114 Safari/537.36', '2021-06-27 11:10:49', '1');
INSERT INTO `db_token` VALUES ('3101', '1', 'admin', 'PP7OQ8wX9vwFv2qOn+mWj31hd8QXL/XZCPMfvbroqj6TrT9uJGYC/jtSxTMrcS7xPfbJKlYUc3wIUyiOrusdNb8xNsL4r3JmGtOLDjP5f5xX25i+b8YFYbFRcDi0PLMLq4/H0sQura3TCbzvqJKW2Z6utUUojZWdYz9Exk/P4DBZiX2/gX3Gq3hTDIi/LXTVZJHvUWuXsepF8mA+1eFr7AtdFA3SUxk98kNk0Fm/N1hXjTV8nezM2gEcqcx07cap', '222.216.163.172', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/91.0.4472.114 Safari/537.36', '2021-06-27 11:11:12', '1');
INSERT INTO `db_token` VALUES ('3102', '1', 'admin', 'PP7OQ8wX9vwFv2qOn+mWj31hd8QXL/XZCPMfvbroqj6TrT9uJGYC/jtSxTMrcS7xPfbJKlYUc3wIUyiOrusdNb8xNsL4r3JmGtOLDjP5f5xX25i+b8YFYbFRcDi0PLMLq4/H0sQura3TCbzvqJKW2Z6utUUojZWdYz9Exk/P4DBZiX2/gX3Gq3hTDIi/LXTVZJHvUWuXsepF8mA+1eFr7AtdFA3SUxk98kNk0Fm/N1hXjTV8nezM2gEcqcx07cap', '222.216.163.172', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/91.0.4472.114 Safari/537.36', '2021-06-27 11:12:13', '1');
INSERT INTO `db_token` VALUES ('3103', '1', 'admin', 'PP7OQ8wX9vwFv2qOn+mWj31hd8QXL/XZCPMfvbroqj6TrT9uJGYC/jtSxTMrcS7xPfbJKlYUc3wIUyiOrusdNb8xNsL4r3JmGtOLDjP5f5xX25i+b8YFYbFRcDi0PLMLq4/H0sQura3TCbzvqJKW2Z6utUUojZWdYz9Exk/P4DBZiX2/gX3Gq3hTDIi/LXTVZJHvUWuXsepF8mA+1eFr7AtdFA3SUxk98kNk0Fm/N1hXjTV8nezM2gEcqcx07cap', '222.216.163.172', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/91.0.4472.114 Safari/537.36', '2021-06-27 11:14:13', '1');
INSERT INTO `db_token` VALUES ('3105', '1', 'admin', 'PP7OQ8wX9vwFv2qOn+mWj31hd8QXL/XZCPMfvbroqj6TrT9uJGYC/jtSxTMrcS7xPfbJKlYUc3wIUyiOrusdNb8xNsL4r3JmGtOLDjP5f5xX25i+b8YFYbFRcDi0PLMLq4/H0sQura3TCbzvqJKW2Z6utUUojZWdYz9Exk/P4DBZiX2/gX3Gq3hTDIi/LXTVZJHvUWuXsepF8mA+1eFr7AtdFA3SUxk98kNk0Fm/N1hXjTV8nezM2gEcqcx07cap', '222.216.163.172', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/91.0.4472.114 Safari/537.36', '2021-06-27 12:23:22', '1');
INSERT INTO `db_token` VALUES ('3106', '1', 'admin', 'PP7OQ8wX9vwFv2qOn+mWj31hd8QXL/XZCPMfvbroqj6TrT9uJGYC/jtSxTMrcS7xPfbJKlYUc3wIUyiOrusdNb8xNsL4r3JmGtOLDjP5f5xX25i+b8YFYbFRcDi0PLMLq4/H0sQura3TCbzvqJKW2Z6utUUojZWdYz9Exk/P4DBZiX2/gX3Gq3hTDIi/LXTVZJHvUWuXsepF8mA+1eFr7AtdFA3SUxk98kNk0Fm/N1hXjTV8nezM2gEcqcx07cap', '222.216.163.172', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/91.0.4472.114 Safari/537.36', '2021-06-27 12:24:12', '1');
INSERT INTO `db_token` VALUES ('3107', '1', 'admin', 'PP7OQ8wX9vwFv2qOn+mWj31hd8QXL/XZCPMfvbroqj6TrT9uJGYC/jtSxTMrcS7xPfbJKlYUc3wIUyiOrusdNb8xNsL4r3JmGtOLDjP5f5xX25i+b8YFYbFRcDi0PLMLq4/H0sQura3TCbzvqJKW2Z6utUUojZWdYz9Exk/P4DBZiX2/gX3Gq3hTDIi/LXTVZJHvUWuXsepF8mA+1eFr7AtdFA3SUxk98kNk0Fm/N1hXjTV8nezM2gEcqcx07cap', '222.216.163.172', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/91.0.4472.114 Safari/537.36', '2021-06-27 12:25:45', '1');
INSERT INTO `db_token` VALUES ('3108', '1', 'admin', 'PP7OQ8wX9vwFv2qOn+mWj31hd8QXL/XZCPMfvbroqj6TrT9uJGYC/jtSxTMrcS7xPfbJKlYUc3wIUyiOrusdNb8xNsL4r3JmGtOLDjP5f5xX25i+b8YFYbFRcDi0PLMLq4/H0sQura3TCbzvqJKW2Z6utUUojZWdYz9Exk/P4DBZiX2/gX3Gq3hTDIi/LXTVZJHvUWuXsepF8mA+1eFr7AtdFA3SUxk98kNk0Fm/N1hXjTV8nezM2gEcqcx07cap', '222.216.163.172', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/91.0.4472.114 Safari/537.36', '2021-06-27 12:35:21', '1');
INSERT INTO `db_token` VALUES ('3109', '1', 'admin', 'PP7OQ8wX9vwFv2qOn+mWj31hd8QXL/XZCPMfvbroqj6TrT9uJGYC/jtSxTMrcS7xPfbJKlYUc3wIUyiOrusdNb8xNsL4r3JmGtOLDjP5f5xX25i+b8YFYbFRcDi0PLMLq4/H0sQura3TCbzvqJKW2Z6utUUojZWdYz9Exk/P4DBZiX2/gX3Gq3hTDIi/LXTVZJHvUWuXsepF8mA+1eFr7AtdFA3SUxk98kNk0Fm/N1hXjTV8nezM2gEcqcx07cap', '222.216.163.172', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/91.0.4472.114 Safari/537.36', '2021-06-27 14:08:13', '1');
INSERT INTO `db_token` VALUES ('3115', '1', 'admin', 'PP7OQ8wX9vwFv2qOn+mWj31hd8QXL/XZCPMfvbroqj6TrT9uJGYC/jtSxTMrcS7xPfbJKlYUc3wIUyiOrusdNb8xNsL4r3JmGtOLDjP5f5xX25i+b8YFYbFRcDi0PLMLq4/H0sQura3TCbzvqJKW2Z6utUUojZWdYz9Exk/P4DBZiX2/gX3Gq3hTDIi/LXTVZJHvUWuXsepF8mA+1eFr7AtdFA3SUxk98kNk0Fm/N1hXjTV8nezM2gEcqcx07cap', '222.216.163.172', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/91.0.4472.114 Safari/537.36', '2021-06-28 09:31:55', '1');
INSERT INTO `db_token` VALUES ('3119', '1', 'admin', 'PP7OQ8wX9vwFv2qOn+mWj31hd8QXL/XZCPMfvbroqj6EoC7zs9DJiTemaUJoOww3PfbJKlYUc3wIUyiOrusdNb8xNsL4r3JmGtOLDjP5f5xX25i+b8YFYbFRcDi0PLMLq4/H0sQura3TCbzvqJKW2Z6utUUojZWdYz9Exk/P4DBZiX2/gX3Gq3hTDIi/LXTV7FVBJBg+fSl5JuZSsUPF+gtdFA3SUxk98kNk0Fm/N1hXjTV8nezM2gEcqcx07cap', '180.140.161.205', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/91.0.4472.124 Safari/537.36', '2021-07-01 09:29:04', '1');
INSERT INTO `db_token` VALUES ('3120', '1', 'admin', 'PP7OQ8wX9vwFv2qOn+mWj31hd8QXL/XZCPMfvbroqj6EoC7zs9DJiTemaUJoOww3PfbJKlYUc3wIUyiOrusdNb8xNsL4r3JmGtOLDjP5f5xX25i+b8YFYbFRcDi0PLMLq4/H0sQura3TCbzvqJKW2Z6utUUojZWdYz9Exk/P4DBZiX2/gX3Gq3hTDIi/LXTVZJHvUWuXsepF8mA+1eFr7AtdFA3SUxk98kNk0Fm/N1hXjTV8nezM2gEcqcx07cap', '180.140.161.205', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/91.0.4472.114 Safari/537.36', '2021-07-01 09:59:27', '1');
INSERT INTO `db_token` VALUES ('3121', '1', 'admin', '++GnsW+7EsFlM1Q1cXncXaAvhcVsDEcDXh6pQFmmZdZY3xtHiXwHuGTMHoTKMsNh28j20VZy2JYC6mz3cl36aAhtQ35x5KtSY0tgw22L9fTFpatNT427vn9Llc+2H/TWNmSDMOKdNp0Tp3A3QK1SWAR9B4CQp/hZDNTZpJWfrhTYly79c/LzF5WSlB4Yjh+cGSe07KEjTeE3+k22s0F5ZmJpJ6tXPwa5zg0cHQfks1A=', '::1', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/91.0.4472.114 Safari/537.36', '2021-07-01 15:58:15', '1');
INSERT INTO `db_token` VALUES ('3122', '1', 'admin', '++GnsW+7EsFlM1Q1cXncXaAvhcVsDEcDXh6pQFmmZdZY3xtHiXwHuGTMHoTKMsNh28j20VZy2JYC6mz3cl36aAhtQ35x5KtSY0tgw22L9fTFpatNT427vn9Llc+2H/TWNmSDMOKdNp0Tp3A3QK1SWAR9B4CQp/hZDNTZpJWfrhTYly79c/LzF5WSlB4Yjh+cGSe07KEjTeE3+k22s0F5ZmJpJ6tXPwa5zg0cHQfks1A=', '::1', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/91.0.4472.114 Safari/537.36', '2021-07-01 15:58:50', '1');
INSERT INTO `db_token` VALUES ('3123', '1', 'admin', '++GnsW+7EsFlM1Q1cXncXaAvhcVsDEcDXh6pQFmmZdZY3xtHiXwHuGTMHoTKMsNh28j20VZy2JYC6mz3cl36aAhtQ35x5KtSY0tgw22L9fTFpatNT427vn9Llc+2H/TWNmSDMOKdNp0Tp3A3QK1SWAR9B4CQp/hZDNTZpJWfrhTYly79c/LzF5WSlB4Yjh+cGSe07KEjTeE3+k22s0F5ZmJpJ6tXPwa5zg0cHQfks1A=', '::1', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/91.0.4472.114 Safari/537.36', '2021-07-01 15:58:53', '1');
INSERT INTO `db_token` VALUES ('3124', '1', 'admin', '++GnsW+7EsFlM1Q1cXncXaAvhcVsDEcDXh6pQFmmZdZY3xtHiXwHuGTMHoTKMsNh28j20VZy2JYC6mz3cl36aAhtQ35x5KtSY0tgw22L9fTFpatNT427vn9Llc+2H/TWNmSDMOKdNp0Tp3A3QK1SWAR9B4CQp/hZDNTZpJWfrhTYly79c/LzF5WSlB4Yjh+cGSe07KEjTeE3+k22s0F5ZmJpJ6tXPwa5zg0cHQfks1A=', '::1', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/91.0.4472.114 Safari/537.36', '2021-07-01 15:59:44', '1');
INSERT INTO `db_token` VALUES ('3125', '1', 'admin', '++GnsW+7EsFlM1Q1cXncXaAvhcVsDEcDXh6pQFmmZdZY3xtHiXwHuGTMHoTKMsNh28j20VZy2JYC6mz3cl36aAhtQ35x5KtSY0tgw22L9fTFpatNT427vn9Llc+2H/TWNmSDMOKdNp0Tp3A3QK1SWAR9B4CQp/hZDNTZpJWfrhTYly79c/LzF5WSlB4Yjh+cGSe07KEjTeE3+k22s0F5ZmJpJ6tXPwa5zg0cHQfks1A=', '::1', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/91.0.4472.114 Safari/537.36', '2021-07-01 16:44:05', '1');
INSERT INTO `db_token` VALUES ('3126', '1', 'admin', '++GnsW+7EsFlM1Q1cXncXaAvhcVsDEcDXh6pQFmmZdZY3xtHiXwHuGTMHoTKMsNh28j20VZy2JYC6mz3cl36aAhtQ35x5KtSY0tgw22L9fTFpatNT427vn9Llc+2H/TWNmSDMOKdNp0Tp3A3QK1SWAR9B4CQp/hZDNTZpJWfrhTYly79c/LzF5WSlB4Yjh+cGSe07KEjTeE3+k22s0F5ZhYX22ZwO4NXN615c5p42V0H0ZThh3unKOJ6YANWavnb', '::1', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/91.0.4472.114 Safari/537.36 Edg/91.0.864.59', '2021-07-01 21:17:56', '1');
INSERT INTO `db_token` VALUES ('3127', '1', 'admin', '++GnsW+7EsFlM1Q1cXncXaAvhcVsDEcDXh6pQFmmZdZY3xtHiXwHuGTMHoTKMsNh28j20VZy2JYC6mz3cl36aAhtQ35x5KtSY0tgw22L9fTFpatNT427vn9Llc+2H/TWNmSDMOKdNp0Tp3A3QK1SWAR9B4CQp/hZDNTZpJWfrhTYly79c/LzF5WSlB4Yjh+cGSe07KEjTeE3+k22s0F5ZhYX22ZwO4NXN615c5p42V0H0ZThh3unKOJ6YANWavnb', '::1', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/91.0.4472.114 Safari/537.36 Edg/91.0.864.59', '2021-07-01 21:18:06', '1');
INSERT INTO `db_token` VALUES ('3128', '1', 'admin', '++GnsW+7EsFlM1Q1cXncXaAvhcVsDEcDXh6pQFmmZdZY3xtHiXwHuGTMHoTKMsNh28j20VZy2JYC6mz3cl36aAhtQ35x5KtSY0tgw22L9fTFpatNT427vn9Llc+2H/TWNmSDMOKdNp0Tp3A3QK1SWAR9B4CQp/hZDNTZpJWfrhTYly79c/LzF5WSlB4Yjh+cGSe07KEjTeE3+k22s0F5ZhYX22ZwO4NXN615c5p42V0H0ZThh3unKOJ6YANWavnb', '::1', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/91.0.4472.114 Safari/537.36 Edg/91.0.864.59', '2021-07-01 21:18:18', '1');
INSERT INTO `db_token` VALUES ('3129', '1', 'admin', '++GnsW+7EsFlM1Q1cXncXaAvhcVsDEcDXh6pQFmmZdZY3xtHiXwHuGTMHoTKMsNh28j20VZy2JYC6mz3cl36aAhtQ35x5KtSY0tgw22L9fTFpatNT427vn9Llc+2H/TWNmSDMOKdNp0Tp3A3QK1SWAR9B4CQp/hZDNTZpJWfrhTYly79c/LzF5WSlB4Yjh+cGSe07KEjTeE3+k22s0F5ZhYX22ZwO4NXN615c5p42V0H0ZThh3unKOJ6YANWavnb', '::1', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/91.0.4472.114 Safari/537.36 Edg/91.0.864.59', '2021-07-01 21:18:42', '1');
INSERT INTO `db_token` VALUES ('3130', '1', 'admin', '++GnsW+7EsFlM1Q1cXncXaAvhcVsDEcDXh6pQFmmZdZY3xtHiXwHuGTMHoTKMsNh28j20VZy2JYC6mz3cl36aAhtQ35x5KtSY0tgw22L9fTFpatNT427vn9Llc+2H/TWNmSDMOKdNp0Tp3A3QK1SWAR9B4CQp/hZDNTZpJWfrhTYly79c/LzF5WSlB4Yjh+cGSe07KEjTeE3+k22s0F5ZhYX22ZwO4NXN615c5p42V0H0ZThh3unKOJ6YANWavnb', '::1', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/91.0.4472.114 Safari/537.36 Edg/91.0.864.59', '2021-07-01 21:20:54', '1');
INSERT INTO `db_token` VALUES ('3131', '1', 'admin', '++GnsW+7EsFlM1Q1cXncXaAvhcVsDEcDXh6pQFmmZdZY3xtHiXwHuGTMHoTKMsNh28j20VZy2JYC6mz3cl36aAhtQ35x5KtSY0tgw22L9fTFpatNT427vn9Llc+2H/TWNmSDMOKdNp0Tp3A3QK1SWAR9B4CQp/hZDNTZpJWfrhTYly79c/LzF5WSlB4Yjh+cGSe07KEjTeE3+k22s0F5ZhYX22ZwO4NXN615c5p42V0H0ZThh3unKOJ6YANWavnb', '::1', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/91.0.4472.114 Safari/537.36 Edg/91.0.864.59', '2021-07-01 21:27:15', '1');
INSERT INTO `db_token` VALUES ('3132', '1', 'admin', '++GnsW+7EsFlM1Q1cXncXaAvhcVsDEcDXh6pQFmmZdZY3xtHiXwHuGTMHoTKMsNh28j20VZy2JYC6mz3cl36aAhtQ35x5KtSY0tgw22L9fTFpatNT427vn9Llc+2H/TWNmSDMOKdNp0Tp3A3QK1SWAR9B4CQp/hZDNTZpJWfrhTYly79c/LzF5WSlB4Yjh+cGSe07KEjTeE3+k22s0F5ZhYX22ZwO4NXN615c5p42V0H0ZThh3unKOJ6YANWavnb', '::1', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/91.0.4472.114 Safari/537.36 Edg/91.0.864.59', '2021-07-01 21:28:36', '1');
INSERT INTO `db_token` VALUES ('3133', '1', 'admin', '++GnsW+7EsFlM1Q1cXncXaAvhcVsDEcDXh6pQFmmZdZY3xtHiXwHuGTMHoTKMsNh28j20VZy2JYC6mz3cl36aAhtQ35x5KtSY0tgw22L9fTFpatNT427vn9Llc+2H/TWNmSDMOKdNp0Tp3A3QK1SWAR9B4CQp/hZDNTZpJWfrhTYly79c/LzF5WSlB4Yjh+cGSe07KEjTeE3+k22s0F5ZhYX22ZwO4NXN615c5p42V0H0ZThh3unKOJ6YANWavnb', '::1', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/91.0.4472.114 Safari/537.36 Edg/91.0.864.59', '2021-07-01 21:29:25', '1');
INSERT INTO `db_token` VALUES ('3134', '1', 'admin', '++GnsW+7EsFlM1Q1cXncXaAvhcVsDEcDXh6pQFmmZdZY3xtHiXwHuGTMHoTKMsNh28j20VZy2JYC6mz3cl36aAhtQ35x5KtSY0tgw22L9fTFpatNT427vn9Llc+2H/TWNmSDMOKdNp0Tp3A3QK1SWAR9B4CQp/hZDNTZpJWfrhTYly79c/LzF5WSlB4Yjh+cGSe07KEjTeE3+k22s0F5ZhYX22ZwO4NXN615c5p42V0H0ZThh3unKOJ6YANWavnb', '::1', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/91.0.4472.114 Safari/537.36 Edg/91.0.864.59', '2021-07-01 22:34:37', '1');
INSERT INTO `db_token` VALUES ('3135', '1', 'admin', '++GnsW+7EsFlM1Q1cXncXaAvhcVsDEcDXh6pQFmmZdZY3xtHiXwHuGTMHoTKMsNh28j20VZy2JYC6mz3cl36aAhtQ35x5KtSY0tgw22L9fTFpatNT427vn9Llc+2H/TWNmSDMOKdNp0Tp3A3QK1SWAR9B4CQp/hZDNTZpJWfrhTYly79c/LzF5WSlB4Yjh+cGSe07KEjTeE3+k22s0F5ZhYX22ZwO4NXN615c5p42V0H0ZThh3unKOJ6YANWavnb', '::1', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/91.0.4472.114 Safari/537.36 Edg/91.0.864.59', '2021-07-02 00:28:10', '1');
INSERT INTO `db_token` VALUES ('3143', '1', 'admin', 'PP7OQ8wX9vwFv2qOn+mWj31hd8QXL/XZCPMfvbroqj6iBEy5riLPNJG3EUzbb7dpiWG3dnm0gGqKkT5S1fDxlSu+hGUI3wHTKmeXDmxK83YheFkcq+Mw4GCDtlhzRUeeTb7rl8S6W4iTF9nFQhdXD+z8l4mqaVZVuu4gd05hg3lfxyDAl66YAgfpQTkOtfyMEN7sSdATwZVykFOUw4eokOIRhtJ7ThNwhW/xoa0l1PxlURKzqhHQCPr3iZfKRBWl', '171.104.221.15', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/91.0.4472.124 Safari/537.36', '2021-07-12 12:16:20', '1');
INSERT INTO `db_token` VALUES ('3148', '1', 'admin', '++GnsW+7EsFlM1Q1cXncXaAvhcVsDEcDXh6pQFmmZdZY3xtHiXwHuGTMHoTKMsNh28j20VZy2JYC6mz3cl36aAhtQ35x5KtSY0tgw22L9fTFpatNT427vn9Llc+2H/TWNmSDMOKdNp0Tp3A3QK1SWAR9B4CQp/hZDNTZpJWfrhSu8CVWVp4XgWdWOFMHSr/E52xrZTacW/cKV7ECxSeQsBEogjLaD+PnMPMPR53Jls5j8BBnBLXIJxkmIoaAsX1a', '::1', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/92.0.4515.107 Safari/537.36 Edg/92.0.902.55', '2021-07-29 21:57:50', '1');
INSERT INTO `db_token` VALUES ('3149', '1', 'admin', '++GnsW+7EsFlM1Q1cXncXaAvhcVsDEcDXh6pQFmmZdZY3xtHiXwHuGTMHoTKMsNhTuFu79gZGXKXj5fLiddQvp7B8PQyr3nRw2w28Oa/io0tfMW2GWp/nUw9STbngGfNcJ5KJizOX7g1mahFiVCxbZwt8teD/bgEPqdQuoRLCMgPXDw4nmAiL15ovHWi5cdU6ITAzHckdyOiQhc6UH1/TQx76G0rPptbZMC94GUHOBR4tiPibjOL4HOd2Dj+fTrCV401fJ3szNoBHKnMdO3GqQ==', null, null, '2021-07-30 11:04:22', '0');
INSERT INTO `db_token` VALUES ('3150', '11', 'aaa', '++GnsW+7EsFlM1Q1cXncXStAxtc8N82HerCDLCtgab6EiWxJXMJJwg6SSUe2YcrTfifVY01kgSBCUK4vI5bwMJoHbGJ3tqo6ulYy7ioHyfhlwaOwGJX2U4OoVEiCvACuSb1n+ZWt++Sg8723WjJQcPiJdSweuugUsHkpQ3CoRW3G4i6n/trSNGJsTOf3ZOxMMCks4XkbQNhFCBA24R3SfSLga6r8Rg1/KVZfCA5h7c3nMaenHbVbK0En1yy1Ix0t', null, null, '2021-07-31 16:30:58', '0');
INSERT INTO `db_token` VALUES ('3151', '1', 'admin', '++GnsW+7EsFlM1Q1cXncXaAvhcVsDEcDXh6pQFmmZdZY3xtHiXwHuGTMHoTKMsNhTuFu79gZGXKXj5fLiddQvp7B8PQyr3nRw2w28Oa/io0tfMW2GWp/nUw9STbngGfNcJ5KJizOX7g1mahFiVCxbZwt8teD/bgEPqdQuoRLCMgPXDw4nmAiL15ovHWi5cdU6ITAzHckdyOiQhc6UH1/TQx76G0rPptbZMC94GUHOBR4tiPibjOL4HOd2Dj+fTrCV401fJ3szNoBHKnMdO3GqQ==', null, null, '2021-07-31 17:18:58', '0');
INSERT INTO `db_token` VALUES ('3152', '1', 'admin', '++GnsW+7EsFlM1Q1cXncXaAvhcVsDEcDXh6pQFmmZdZY3xtHiXwHuGTMHoTKMsNhTuFu79gZGXKXj5fLiddQvp7B8PQyr3nRw2w28Oa/io0tfMW2GWp/nUw9STbngGfNcJ5KJizOX7g1mahFiVCxbZwt8teD/bgEPqdQuoRLCMgPXDw4nmAiL15ovHWi5cdU6ITAzHckdyOiQhc6UH1/TQx76G0rPptbZMC94GUHOBR4tiPibjOL4HOd2Dj+fTrCV401fJ3szNoBHKnMdO3GqQ==', null, null, '2021-08-01 15:41:33', '0');
INSERT INTO `db_token` VALUES ('3153', '1', 'admin', '++GnsW+7EsFlM1Q1cXncXaAvhcVsDEcDXh6pQFmmZdZY3xtHiXwHuGTMHoTKMsNhTuFu79gZGXKXj5fLiddQvp7B8PQyr3nRw2w28Oa/io0tfMW2GWp/nUw9STbngGfNcJ5KJizOX7g1mahFiVCxbZwt8teD/bgEPqdQuoRLCMgPXDw4nmAiL15ovHWi5cdU6ITAzHckdyOiQhc6UH1/TQx76G0rPptbZMC94GUHOBR4tiPibjOL4HOd2Dj+fTrCV401fJ3szNoBHKnMdO3GqQ==', null, null, '2021-07-29 17:35:53', '0');
INSERT INTO `db_token` VALUES ('3154', '1', 'admin', '++GnsW+7EsFlM1Q1cXncXaAvhcVsDEcDXh6pQFmmZdZY3xtHiXwHuGTMHoTKMsNhTuFu79gZGXKXj5fLiddQvp7B8PQyr3nRw2w28Oa/io0tfMW2GWp/nUw9STbngGfNcJ5KJizOX7g1mahFiVCxbZwt8teD/bgEPqdQuoRLCMgPXDw4nmAiL15ovHWi5cdU6ITAzHckdyOiQhc6UH1/TQx76G0rPptbZMC94GUHOBR4tiPibjOL4HOd2Dj+fTrCV401fJ3szNoBHKnMdO3GqQ==', null, null, '2021-08-02 18:24:05', '0');
INSERT INTO `db_token` VALUES ('3155', '1', 'admin', '++GnsW+7EsFlM1Q1cXncXaAvhcVsDEcDXh6pQFmmZdZY3xtHiXwHuGTMHoTKMsNh28j20VZy2JYC6mz3cl36aAhtQ35x5KtSY0tgw22L9fTFpatNT427vn9Llc+2H/TWNmSDMOKdNp0Tp3A3QK1SWAR9B4CQp/hZDNTZpJWfrhSu8CVWVp4XgWdWOFMHSr/E52xrZTacW/cKV7ECxSeQsBEogjLaD+PnMPMPR53Jls4s8VIzvQTWr8LycYdJPS5i', '::1', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/92.0.4515.107 Safari/537.36 Edg/92.0.902.62', '2021-08-04 06:55:53', '1');
INSERT INTO `db_token` VALUES ('3156', '1', 'admin', '++GnsW+7EsFlM1Q1cXncXaAvhcVsDEcDXh6pQFmmZdZY3xtHiXwHuGTMHoTKMsNhTuFu79gZGXKXj5fLiddQvp7B8PQyr3nRw2w28Oa/io0tfMW2GWp/nUw9STbngGfNcJ5KJizOX7g1mahFiVCxbZwt8teD/bgEPqdQuoRLCMgPXDw4nmAiL15ovHWi5cdU6ITAzHckdyOiQhc6UH1/TQx76G0rPptbZMC94GUHOBR4tiPibjOL4HOd2Dj+fTrCV401fJ3szNoBHKnMdO3GqQ==', null, null, '2021-08-29 10:58:21', '0');

-- ----------------------------
-- Table structure for db_users
-- ----------------------------
DROP TABLE IF EXISTS `db_users`;
CREATE TABLE `db_users` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `userid` varchar(50) NOT NULL,
  `recode` varchar(50) DEFAULT NULL COMMENT '推广码',
  `password` varchar(50) NOT NULL,
  `password2` varchar(50) NOT NULL,
  `tx` longtext,
  `username` varchar(50) DEFAULT NULL,
  `usertel` varchar(50) DEFAULT NULL,
  `usercode` varchar(50) DEFAULT NULL,
  `rdt` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `pdt` datetime NOT NULL DEFAULT '0000-00-00 00:00:00',
  `ulevel` int(11) NOT NULL DEFAULT '0',
  `ylevel` int(11) NOT NULL DEFAULT '0',
  `xlevel` int(11) NOT NULL DEFAULT '0',
  `isbd` int(11) NOT NULL DEFAULT '0',
  `bdlevel` int(11) NOT NULL DEFAULT '0',
  `bdsheng` varchar(50) DEFAULT NULL,
  `bdshi` varchar(50) DEFAULT NULL,
  `bdxian` varchar(50) DEFAULT NULL,
  `bdaddress` longtext,
  `bddate` datetime NOT NULL DEFAULT '0000-00-00 00:00:00',
  `lsk` decimal(18,2) NOT NULL DEFAULT '0.00',
  `ylsk` decimal(18,2) NOT NULL DEFAULT '0.00',
  `pv` decimal(18,2) NOT NULL DEFAULT '0.00',
  `dan` int(11) NOT NULL DEFAULT '0',
  `tdan` int(11) NOT NULL DEFAULT '0',
  `ispay` int(11) NOT NULL DEFAULT '0',
  `islock` int(11) NOT NULL DEFAULT '0',
  `reid` int(11) NOT NULL DEFAULT '0',
  `rename` varchar(50) DEFAULT NULL,
  `reusername` varchar(50) DEFAULT NULL,
  `repath` longtext,
  `relevel` int(11) NOT NULL DEFAULT '0',
  `recount` int(11) NOT NULL DEFAULT '0',
  `teamcount` int(11) NOT NULL DEFAULT '0',
  `reyeji` decimal(18,2) NOT NULL DEFAULT '0.00',
  `teamyeji` decimal(18,2) NOT NULL DEFAULT '0.00',
  `daquyeji` decimal(18,2) NOT NULL DEFAULT '0.00',
  `xiaoquyeji` decimal(18,2) NOT NULL DEFAULT '0.00',
  `bdid` int(11) NOT NULL DEFAULT '0',
  `bduserid` varchar(50) DEFAULT NULL,
  `bdusername` varchar(50) DEFAULT NULL,
  `iswx` int(11) NOT NULL DEFAULT '0',
  `wxtoken` longtext,
  `wxnickname` varchar(100) DEFAULT NULL,
  `wxheadimgurl` longtext,
  `msgdate` datetime NOT NULL DEFAULT '0000-00-00 00:00:00',
  `monthyeji` decimal(18,2) NOT NULL DEFAULT '0.00',
  `shouru` decimal(18,2) NOT NULL DEFAULT '0.00',
  `jid` int(11) NOT NULL DEFAULT '0',
  `juserid` varchar(50) DEFAULT NULL,
  `jusername` varchar(50) DEFAULT NULL,
  `zzxz` int(11) NOT NULL DEFAULT '1' COMMENT '转账限制，1限制，0不限制',
  PRIMARY KEY (`id`),
  UNIQUE KEY `id` (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=14 DEFAULT CHARSET=utf8mb4;

-- ----------------------------
-- Records of db_users
-- ----------------------------
INSERT INTO `db_users` VALUES ('1', 'admin', 'A10000', '96e79218965eb72c92a549dd5a330112', '96e79218965eb72c92a549dd5a330112', 'tx.png', '管理员', '15000001211', '26423465415465168465', '2020-07-28 20:52:21', '2020-07-28 20:52:21', '1', '1', '0', '1', '1', '天津市', '天津市', '和平区', '111', '2021-06-24 11:23:20', '0.00', '0.00', '0.00', '0', '3', '1', '0', '0', '-', '-', ',', '0', '2', '3', '532.00', '798.00', '532.00', '266.00', '0', 'admin', '1', '1', 'oluXYt3WNpxxREpRP4-F2iTPB-1M', '弗兰克', 'http://thirdwx.qlogo.cn/mmopen/vi_32/X4m23uuuWPR5lI1ibicIpILPwuEZ5qWl9S4VRialkZx4P1uCNa3V2C6FxKGXw9l8UCGGiaCoylqHu9f7QQhz2kxKQg/132', '2021-08-26 11:34:31', '798.00', '0.00', '0', '', null, '1');
INSERT INTO `db_users` VALUES ('11', 'aaa', 'XDDW16', 'e10adc3949ba59abbe56e057f20f883e', 'e10adc3949ba59abbe56e057f20f883e', 'tx.png', '1', '1', null, '2021-07-23 16:20:09', '2021-07-23 16:20:19', '1', '1', '0', '0', '0', null, null, null, null, '2021-07-23 16:20:09', '266.00', '266.00', '0.00', '0', '1', '1', '0', '1', 'admin', '管理员', ',1,', '1', '1', '1', '266.00', '266.00', '266.00', '0.00', '1', 'admin', '管理员', '0', '', null, null, '2021-08-26 11:30:57', '266.00', '0.00', '0', '', null, '1');
INSERT INTO `db_users` VALUES ('12', 'bbb', 'JKL3LF', 'e10adc3949ba59abbe56e057f20f883e', 'e10adc3949ba59abbe56e057f20f883e', 'tx.png', '123456', '123456', null, '2021-07-23 16:20:58', '2021-07-23 16:21:21', '1', '1', '0', '0', '0', null, null, null, null, '2021-07-23 16:20:58', '266.00', '266.00', '0.00', '0', '0', '1', '0', '11', 'aaa', '1', ',1,11,', '2', '0', '0', '0.00', '0.00', '0.00', '0.00', '1', 'admin', '管理员', '0', '', null, null, '1970-01-01 00:00:00', '0.00', '0.00', '0', '', null, '1');
INSERT INTO `db_users` VALUES ('13', 'ccc', '6Z6BFN', 'e10adc3949ba59abbe56e057f20f883e', 'e10adc3949ba59abbe56e057f20f883e', 'tx.png', '123456', '123456', '', '2021-07-23 16:21:15', '2021-07-23 16:21:25', '1', '1', '1', '0', '0', null, null, null, null, '2021-07-23 16:21:15', '266.00', '266.00', '0.00', '0', '0', '1', '0', '1', 'admin', '管理员', ',1,', '1', '0', '0', '0.00', '0.00', '0.00', '0.00', '1', 'admin', '管理员', '0', '', null, null, '1970-01-01 00:00:00', '0.00', '0.00', '0', '', null, '1');

-- ----------------------------
-- Table structure for db_users_address
-- ----------------------------
DROP TABLE IF EXISTS `db_users_address`;
CREATE TABLE `db_users_address` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `uid` int(11) NOT NULL DEFAULT '0',
  `userid` varchar(50) NOT NULL,
  `username` varchar(50) DEFAULT NULL,
  `usertel` varchar(50) DEFAULT NULL,
  `sheng` varchar(50) DEFAULT NULL,
  `shi` varchar(50) DEFAULT NULL,
  `xian` varchar(50) DEFAULT NULL,
  `address` longtext,
  `isdefault` int(11) NOT NULL DEFAULT '0',
  `odate` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `areaCode` varchar(50) DEFAULT NULL,
  PRIMARY KEY (`id`),
  KEY `uid` (`uid`),
  CONSTRAINT `db_users_address_ibfk_1` FOREIGN KEY (`uid`) REFERENCES `db_users` (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=215 DEFAULT CHARSET=utf8mb4;

-- ----------------------------
-- Records of db_users_address
-- ----------------------------
INSERT INTO `db_users_address` VALUES ('115', '1', 'admin', '刘', '14712001400', '北京市', '北京市', '西城区', '584', '0', '2020-06-18 18:04:20', '110102');
INSERT INTO `db_users_address` VALUES ('118', '1', 'admin', '王五', '14712001200', '北京市', '北京市', '朝阳区', '147hao', '0', '2020-06-19 11:34:58', '110105');
INSERT INTO `db_users_address` VALUES ('198', '1', 'admin', 'lisi', '14712001200', '天津市', '天津市', '红桥区', 'admin', '0', '2020-08-31 14:26:15', '120106');
INSERT INTO `db_users_address` VALUES ('199', '1', 'admin', '路人甲', '13848747989', '内蒙古自治区', '呼和浩特市', '新城区', '在街头走一走', '0', '2020-08-31 17:35:58', '150102');
INSERT INTO `db_users_address` VALUES ('211', '1', 'admin', '121', '14712001200', '北京市', '北京市', '朝阳区', '23232', '0', '2021-02-06 16:06:48', '110105');
INSERT INTO `db_users_address` VALUES ('212', '1', 'admin', '21321', '15659849889', '北京市', '北京市', '东城区', '123', '0', '2021-03-10 14:37:45', '110101');
INSERT INTO `db_users_address` VALUES ('214', '1', 'admin', '1', '12312313133', '北京市', '北京市', '东城区', '123', '0', '2021-06-25 15:12:29', '110101');

-- ----------------------------
-- Table structure for db_users_bank
-- ----------------------------
DROP TABLE IF EXISTS `db_users_bank`;
CREATE TABLE `db_users_bank` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `uid` int(11) NOT NULL DEFAULT '0',
  `userid` varchar(50) NOT NULL,
  `username` varchar(50) DEFAULT NULL,
  `usertel` varchar(50) DEFAULT NULL,
  `bankname` varchar(50) DEFAULT NULL,
  `bankcard` varchar(50) DEFAULT NULL,
  `bankaddress` varchar(100) DEFAULT NULL,
  `isdefault` int(11) NOT NULL DEFAULT '0',
  `bdate` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `bankimg` varchar(200) DEFAULT NULL COMMENT '收款图（二维码）',
  PRIMARY KEY (`id`),
  KEY `uid` (`uid`),
  CONSTRAINT `db_users_bank_ibfk_1` FOREIGN KEY (`uid`) REFERENCES `db_users` (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=35 DEFAULT CHARSET=utf8mb4;

-- ----------------------------
-- Records of db_users_bank
-- ----------------------------
INSERT INTO `db_users_bank` VALUES ('33', '1', 'admin', '刘', '1111111', '支付宝', '1111111111', null, '0', '2020-07-04 08:30:30', 'admined2af836-a04b-4914-970f-69711fca895b..jpg');
INSERT INTO `db_users_bank` VALUES ('34', '11', 'aaa', '123', '123123213', '农业银行', '123', '', '1', '2021-07-28 16:32:45', '');

-- ----------------------------
-- Table structure for db_users_delete
-- ----------------------------
DROP TABLE IF EXISTS `db_users_delete`;
CREATE TABLE `db_users_delete` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `uid` int(11) NOT NULL DEFAULT '0',
  `userid` varchar(50) NOT NULL,
  `username` varchar(50) DEFAULT NULL,
  `reid` int(11) NOT NULL DEFAULT '0',
  `rename` varchar(50) DEFAULT NULL,
  `repath` longtext,
  `relevel` int(11) NOT NULL DEFAULT '0',
  `recount` int(11) NOT NULL DEFAULT '0',
  `teamcount` int(11) NOT NULL DEFAULT '0',
  `ddate` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `deluid` int(11) DEFAULT NULL,
  `deluserid` varchar(50) DEFAULT NULL,
  `delip` varchar(50) DEFAULT NULL,
  `dellx` int(11) DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- ----------------------------
-- Records of db_users_delete
-- ----------------------------

-- ----------------------------
-- Table structure for db_users_fteam
-- ----------------------------
DROP TABLE IF EXISTS `db_users_fteam`;
CREATE TABLE `db_users_fteam` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `uid` int(11) NOT NULL DEFAULT '0',
  `userid` varchar(50) NOT NULL,
  `username` varchar(50) DEFAULT NULL,
  `fatherid` int(11) NOT NULL DEFAULT '0',
  `fathername` varchar(50) DEFAULT NULL,
  `ftreeplace` int(11) NOT NULL DEFAULT '0',
  `fpath` longtext,
  `flevel` int(11) NOT NULL DEFAULT '0',
  `fcount` int(11) NOT NULL DEFAULT '0',
  `teamcount` int(11) NOT NULL DEFAULT '0',
  `teamyeji` decimal(18,2) NOT NULL DEFAULT '0.00',
  `fdate` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `area1` decimal(18,2) NOT NULL DEFAULT '0.00',
  `area2` decimal(18,2) NOT NULL DEFAULT '0.00',
  `area3` decimal(18,2) NOT NULL DEFAULT '0.00',
  `area4` decimal(18,2) NOT NULL DEFAULT '0.00',
  `area5` decimal(18,2) NOT NULL DEFAULT '0.00',
  `yarea1` decimal(18,2) NOT NULL DEFAULT '0.00',
  `yarea2` decimal(18,2) NOT NULL DEFAULT '0.00',
  `yarea3` decimal(18,2) NOT NULL DEFAULT '0.00',
  `yarea4` decimal(18,2) NOT NULL DEFAULT '0.00',
  `yarea5` decimal(18,2) NOT NULL DEFAULT '0.00',
  `narea1` int(11) NOT NULL DEFAULT '0',
  `narea2` int(11) NOT NULL DEFAULT '0',
  `narea3` int(11) NOT NULL DEFAULT '0',
  `narea4` int(11) NOT NULL DEFAULT '0',
  `narea5` int(11) NOT NULL DEFAULT '0',
  `ch1` int(11) NOT NULL DEFAULT '0',
  `ch2` int(11) NOT NULL DEFAULT '0',
  `ch3` int(11) NOT NULL DEFAULT '0',
  `ch4` int(11) NOT NULL DEFAULT '0',
  `ch5` int(11) NOT NULL DEFAULT '0',
  PRIMARY KEY (`id`),
  KEY `uid` (`uid`),
  CONSTRAINT `db_users_fteam_ibfk_1` FOREIGN KEY (`uid`) REFERENCES `db_users` (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=13 DEFAULT CHARSET=utf8mb4;

-- ----------------------------
-- Records of db_users_fteam
-- ----------------------------
INSERT INTO `db_users_fteam` VALUES ('1', '1', 'admin', '管理员', '0', '无', '1', ',', '0', '0', '3', '798.00', '2019-08-10 06:31:05', '532.00', '266.00', '0.00', '0.00', '0.00', '532.00', '266.00', '0.00', '0.00', '0.00', '2', '1', '0', '0', '0', '11', '13', '0', '0', '0');
INSERT INTO `db_users_fteam` VALUES ('10', '11', 'aaa', '1', '1', 'admin', '1', ',1,', '1', '0', '1', '266.00', '2021-07-23 16:20:09', '266.00', '0.00', '0.00', '0.00', '0.00', '266.00', '0.00', '0.00', '0.00', '0.00', '1', '0', '0', '0', '0', '12', '0', '0', '0', '0');
INSERT INTO `db_users_fteam` VALUES ('11', '12', 'bbb', '123456', '11', 'aaa', '1', ',1,11,', '2', '0', '0', '0.00', '2021-07-23 16:20:58', '0.00', '0.00', '0.00', '0.00', '0.00', '0.00', '0.00', '0.00', '0.00', '0.00', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0');
INSERT INTO `db_users_fteam` VALUES ('12', '13', 'ccc', '123456', '1', 'admin', '2', ',1,', '1', '0', '0', '0.00', '2021-07-23 16:21:16', '0.00', '0.00', '0.00', '0.00', '0.00', '0.00', '0.00', '0.00', '0.00', '0.00', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0');

-- ----------------------------
-- Table structure for db_users_fwzx_apply
-- ----------------------------
DROP TABLE IF EXISTS `db_users_fwzx_apply`;
CREATE TABLE `db_users_fwzx_apply` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `uid` int(11) NOT NULL DEFAULT '0',
  `userid` varchar(50) NOT NULL,
  `username` varchar(50) NOT NULL,
  `bdlevel` int(11) NOT NULL DEFAULT '0',
  `jine` decimal(18,2) NOT NULL DEFAULT '0.00',
  `bdsheng` varchar(50) DEFAULT NULL,
  `bdshi` varchar(50) DEFAULT NULL,
  `bdxian` varchar(50) DEFAULT NULL,
  `bdaddress` longtext,
  `fdate` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `ispay` int(11) NOT NULL DEFAULT '0',
  `sdate` datetime DEFAULT NULL,
  `lx` int(11) DEFAULT '0',
  `bz` text,
  PRIMARY KEY (`id`),
  KEY `uid` (`uid`),
  CONSTRAINT `db_users_fwzx_apply_ibfk_1` FOREIGN KEY (`uid`) REFERENCES `db_users` (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- ----------------------------
-- Records of db_users_fwzx_apply
-- ----------------------------

-- ----------------------------
-- Table structure for db_users_jihuo_record
-- ----------------------------
DROP TABLE IF EXISTS `db_users_jihuo_record`;
CREATE TABLE `db_users_jihuo_record` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `uid` int(11) NOT NULL DEFAULT '0',
  `userid` varchar(50) NOT NULL,
  `username` varchar(50) NOT NULL,
  `hblx` int(11) NOT NULL DEFAULT '0',
  `jine` decimal(18,2) NOT NULL DEFAULT '0.00',
  `cid` int(11) NOT NULL DEFAULT '0' COMMENT '货币id',
  `codename` varchar(50) NOT NULL COMMENT '货币英文名称',
  `coinname` varchar(50) NOT NULL COMMENT '货币中文名称',
  `jid` int(11) NOT NULL DEFAULT '0',
  `juserid` varchar(50) DEFAULT NULL,
  `jusername` varchar(50) DEFAULT NULL,
  `jdate` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP,
  PRIMARY KEY (`id`),
  KEY `uid` (`uid`),
  KEY `jid` (`jid`),
  CONSTRAINT `db_users_jihuo_record_ibfk_1` FOREIGN KEY (`uid`) REFERENCES `db_users` (`id`),
  CONSTRAINT `db_users_jihuo_record_ibfk_2` FOREIGN KEY (`jid`) REFERENCES `db_users` (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- ----------------------------
-- Records of db_users_jihuo_record
-- ----------------------------

-- ----------------------------
-- Table structure for db_users_levelup
-- ----------------------------
DROP TABLE IF EXISTS `db_users_levelup`;
CREATE TABLE `db_users_levelup` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `uid` int(11) NOT NULL DEFAULT '0',
  `userid` varchar(50) NOT NULL,
  `username` varchar(50) DEFAULT NULL,
  `lx` int(11) NOT NULL DEFAULT '0' COMMENT '升级类型,0-ulevel,1-xlevel',
  `ylevel` int(11) NOT NULL DEFAULT '0' COMMENT '原级别',
  `level` int(11) NOT NULL DEFAULT '0' COMMENT '新级别',
  `jine` decimal(18,2) NOT NULL DEFAULT '0.00' COMMENT '升级花费金额',
  `sdate` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `state` int(11) NOT NULL DEFAULT '0' COMMENT '状态,0未审核,1已审核',
  PRIMARY KEY (`id`),
  KEY `uid` (`uid`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- ----------------------------
-- Records of db_users_levelup
-- ----------------------------

-- ----------------------------
-- Table structure for db_wallets
-- ----------------------------
DROP TABLE IF EXISTS `db_wallets`;
CREATE TABLE `db_wallets` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `uid` int(11) NOT NULL DEFAULT '0',
  `userid` varchar(50) NOT NULL,
  `cid` int(11) NOT NULL DEFAULT '0' COMMENT '货币id',
  `cname` varchar(50) NOT NULL COMMENT '货币英文名',
  `cname_zh` varchar(50) NOT NULL COMMENT '货币中文名称',
  `jine` decimal(18,2) NOT NULL DEFAULT '0.00' COMMENT '金额',
  `wdate` datetime NOT NULL DEFAULT '0000-00-00 00:00:00' COMMENT '生成记录时间',
  PRIMARY KEY (`id`),
  KEY `users_wallets_ibfk_1` (`uid`),
  KEY `wallets_ibfk_2` (`cid`),
  CONSTRAINT `db_wallets_ibfk_1` FOREIGN KEY (`uid`) REFERENCES `db_users` (`id`),
  CONSTRAINT `db_wallets_ibfk_2` FOREIGN KEY (`cid`) REFERENCES `db_wallets_coin` (`id`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=153 DEFAULT CHARSET=utf8mb4;

-- ----------------------------
-- Records of db_wallets
-- ----------------------------
INSERT INTO `db_wallets` VALUES ('95', '1', 'admin', '3', 'gwb', '购物币', '1009.60', '2020-10-27 15:17:21');
INSERT INTO `db_wallets` VALUES ('102', '1', 'admin', '1', 'mey', '奖金币', '100.00', '2020-12-15 15:56:11');
INSERT INTO `db_wallets` VALUES ('108', '1', 'admin', '2', 'bdb', '报单币', '50.00', '2020-12-15 16:27:42');
INSERT INTO `db_wallets` VALUES ('121', '1', 'admin', '4', 'qgb', '股权币', '0.00', '2021-06-24 12:04:32');
INSERT INTO `db_wallets` VALUES ('141', '11', 'aaa', '1', 'mey', '奖金币', '1000.00', '2021-07-23 16:20:09');
INSERT INTO `db_wallets` VALUES ('142', '11', 'aaa', '2', 'bdb', '报单币', '0.00', '2021-07-23 16:20:09');
INSERT INTO `db_wallets` VALUES ('143', '11', 'aaa', '3', 'gwb', '购物币', '8754.67', '2021-07-23 16:20:09');
INSERT INTO `db_wallets` VALUES ('144', '11', 'aaa', '4', 'qgb', '股权币', '0.00', '2021-07-23 16:20:09');
INSERT INTO `db_wallets` VALUES ('145', '12', 'bbb', '1', 'mey', '奖金币', '0.00', '2021-07-23 16:20:58');
INSERT INTO `db_wallets` VALUES ('146', '12', 'bbb', '2', 'bdb', '报单币', '0.00', '2021-07-23 16:20:58');
INSERT INTO `db_wallets` VALUES ('147', '12', 'bbb', '3', 'gwb', '购物币', '0.00', '2021-07-23 16:20:58');
INSERT INTO `db_wallets` VALUES ('148', '12', 'bbb', '4', 'qgb', '股权币', '0.00', '2021-07-23 16:20:58');
INSERT INTO `db_wallets` VALUES ('149', '13', 'ccc', '1', 'mey', '奖金币', '0.00', '2021-07-23 16:21:16');
INSERT INTO `db_wallets` VALUES ('150', '13', 'ccc', '2', 'bdb', '报单币', '0.00', '2021-07-23 16:21:16');
INSERT INTO `db_wallets` VALUES ('151', '13', 'ccc', '3', 'gwb', '购物币', '0.00', '2021-07-23 16:21:16');
INSERT INTO `db_wallets` VALUES ('152', '13', 'ccc', '4', 'qgb', '股权币', '0.00', '2021-07-23 16:21:16');

-- ----------------------------
-- Table structure for db_wallets_chongzhi
-- ----------------------------
DROP TABLE IF EXISTS `db_wallets_chongzhi`;
CREATE TABLE `db_wallets_chongzhi` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `uid` int(11) NOT NULL DEFAULT '0',
  `userid` varchar(50) NOT NULL,
  `username` varchar(50) NOT NULL,
  `cid` int(11) NOT NULL DEFAULT '0' COMMENT '货币id',
  `codename` varchar(50) NOT NULL COMMENT '货币英文名称',
  `coinname` varchar(50) NOT NULL COMMENT '货币中文名称',
  `jine` decimal(18,2) NOT NULL DEFAULT '0.00',
  `lx` int(11) NOT NULL DEFAULT '0',
  `usertel` varchar(50) NOT NULL,
  `beizhu` varchar(255) NOT NULL,
  `cdate` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `fdate` datetime NOT NULL,
  `ispay` int(11) NOT NULL DEFAULT '0',
  `isdelete` int(11) NOT NULL DEFAULT '0',
  `czimg` varchar(200) DEFAULT NULL,
  PRIMARY KEY (`id`),
  KEY `uid` (`uid`),
  CONSTRAINT `db_wallets_chongzhi_ibfk_1` FOREIGN KEY (`uid`) REFERENCES `db_users` (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=55 DEFAULT CHARSET=utf8mb4;

-- ----------------------------
-- Records of db_wallets_chongzhi
-- ----------------------------
INSERT INTO `db_wallets_chongzhi` VALUES ('53', '1', 'admin', '管理员', '3', 'gwb', '购物币', '1000.00', '0', '15000001211', '', '2021-07-28 16:30:04', '2021-07-28 16:30:04', '1', '0', '');
INSERT INTO `db_wallets_chongzhi` VALUES ('54', '11', 'aaa', '1', '3', 'gwb', '购物币', '10000.00', '0', '1', '', '2021-07-28 16:31:07', '2021-07-28 16:31:07', '1', '0', '');

-- ----------------------------
-- Table structure for db_wallets_chongzhi_select
-- ----------------------------
DROP TABLE IF EXISTS `db_wallets_chongzhi_select`;
CREATE TABLE `db_wallets_chongzhi_select` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `cid` int(11) NOT NULL DEFAULT '0' COMMENT '货币id',
  `codename` varchar(50) NOT NULL COMMENT '货币英文名称',
  `coinname` varchar(50) NOT NULL COMMENT '货币中文名称',
  `jinemin` decimal(18,2) NOT NULL DEFAULT '1.00' COMMENT '最低充值金额',
  `jinemax` decimal(18,2) NOT NULL DEFAULT '10000.00' COMMENT '最高充值金额',
  `jinebs` int(11) NOT NULL DEFAULT '1' COMMENT '充值倍数',
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=87 DEFAULT CHARSET=utf8mb4;

-- ----------------------------
-- Records of db_wallets_chongzhi_select
-- ----------------------------
INSERT INTO `db_wallets_chongzhi_select` VALUES ('83', '3', 'gwb', '购物币', '1.00', '99999.00', '1');
INSERT INTO `db_wallets_chongzhi_select` VALUES ('84', '1', 'mey', '奖金币', '1.00', '1.00', '1');
INSERT INTO `db_wallets_chongzhi_select` VALUES ('85', '2', 'bdb', '报单币', '1.00', '1.00', '1');
INSERT INTO `db_wallets_chongzhi_select` VALUES ('86', '4', 'qgb', '股权币', '1.00', '1.00', '1');

-- ----------------------------
-- Table structure for db_wallets_coin
-- ----------------------------
DROP TABLE IF EXISTS `db_wallets_coin`;
CREATE TABLE `db_wallets_coin` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `codename` varchar(50) NOT NULL COMMENT '货币英文名称',
  `coinname` varchar(50) NOT NULL COMMENT '货币中文名称',
  `state` int(11) NOT NULL DEFAULT '1' COMMENT '是否使用: 1:使用  0:不使用',
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=5 DEFAULT CHARSET=utf8mb4;

-- ----------------------------
-- Records of db_wallets_coin
-- ----------------------------
INSERT INTO `db_wallets_coin` VALUES ('1', 'mey', '奖金币', '1');
INSERT INTO `db_wallets_coin` VALUES ('2', 'bdb', '报单币', '1');
INSERT INTO `db_wallets_coin` VALUES ('3', 'gwb', '购物币', '1');
INSERT INTO `db_wallets_coin` VALUES ('4', 'qgb', '股权币', '1');

-- ----------------------------
-- Table structure for db_wallets_tixian
-- ----------------------------
DROP TABLE IF EXISTS `db_wallets_tixian`;
CREATE TABLE `db_wallets_tixian` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `uid` int(11) NOT NULL DEFAULT '0',
  `userid` varchar(50) NOT NULL,
  `username` varchar(50) DEFAULT NULL,
  `cid` int(11) NOT NULL DEFAULT '0' COMMENT '货币id',
  `codename` varchar(50) NOT NULL COMMENT '货币英文名称',
  `coinname` varchar(50) NOT NULL COMMENT '货币中文名称',
  `jine` decimal(18,2) NOT NULL DEFAULT '0.00',
  `shouxu` decimal(18,2) NOT NULL DEFAULT '0.00',
  `lx` int(11) NOT NULL DEFAULT '0',
  `bankname` varchar(50) DEFAULT NULL,
  `bankcard` varchar(50) DEFAULT NULL,
  `bankaddress` varchar(255) DEFAULT NULL,
  `bankimg` longtext,
  `usertel` varchar(50) DEFAULT NULL,
  `tdate` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `fdate` datetime NOT NULL,
  `ispay` int(11) NOT NULL DEFAULT '0',
  `isdelete` int(11) NOT NULL DEFAULT '0',
  `beizhu` varchar(255) DEFAULT NULL,
  `chexiaoyuanyin` varchar(255) DEFAULT NULL,
  PRIMARY KEY (`id`),
  KEY `uid` (`uid`),
  CONSTRAINT `db_wallets_tixian_ibfk_1` FOREIGN KEY (`uid`) REFERENCES `db_users` (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=6 DEFAULT CHARSET=utf8mb4;

-- ----------------------------
-- Records of db_wallets_tixian
-- ----------------------------
INSERT INTO `db_wallets_tixian` VALUES ('4', '1', 'admin', '管理员', '3', 'gwb', '购物币', '40.00', '0.40', '0', '支付宝', '1111111111', null, 'admined2af836-a04b-4914-970f-69711fca895b..jpg', '15000001211', '2021-07-28 16:30:31', '2021-07-28 16:30:31', '1', '0', '', null);
INSERT INTO `db_wallets_tixian` VALUES ('5', '11', 'aaa', '1', '3', 'gwb', '购物币', '1233.00', '12.33', '0', '农业银行', '123', '', '', '1', '2021-07-28 16:33:00', '2021-07-28 16:33:00', '1', '0', '', null);

-- ----------------------------
-- Table structure for db_wallets_tixian_select
-- ----------------------------
DROP TABLE IF EXISTS `db_wallets_tixian_select`;
CREATE TABLE `db_wallets_tixian_select` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `cid` int(11) NOT NULL DEFAULT '0' COMMENT '货币id',
  `codename` varchar(50) NOT NULL COMMENT '货币英文名称',
  `coinname` varchar(50) NOT NULL COMMENT '货币中文名称',
  `jinemin` decimal(18,2) NOT NULL DEFAULT '1.00' COMMENT '最低提现金额',
  `jinemax` decimal(18,2) NOT NULL DEFAULT '10000.00' COMMENT '最高提现金额',
  `jinebs` int(11) NOT NULL DEFAULT '1' COMMENT '提现倍数',
  `shouxu` decimal(18,2) NOT NULL DEFAULT '0.00' COMMENT '提现手续费',
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=51 DEFAULT CHARSET=utf8mb4;

-- ----------------------------
-- Records of db_wallets_tixian_select
-- ----------------------------
INSERT INTO `db_wallets_tixian_select` VALUES ('50', '1', 'mey', '奖金币', '1.00', '99999.00', '1', '1.00');

-- ----------------------------
-- Table structure for db_wallets_zengjian
-- ----------------------------
DROP TABLE IF EXISTS `db_wallets_zengjian`;
CREATE TABLE `db_wallets_zengjian` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `uid` int(11) NOT NULL DEFAULT '0',
  `userid` varchar(50) NOT NULL,
  `username` varchar(50) DEFAULT NULL,
  `cid` int(11) NOT NULL DEFAULT '0' COMMENT '货币id',
  `codename` varchar(50) NOT NULL COMMENT '货币英文名称',
  `coinname` varchar(50) NOT NULL COMMENT '货币中文名称',
  `lx` int(11) NOT NULL DEFAULT '0',
  `yjine` decimal(18,2) NOT NULL DEFAULT '0.00',
  `jine` decimal(18,2) NOT NULL DEFAULT '0.00',
  `xjine` decimal(18,2) NOT NULL DEFAULT '0.00',
  `bz` varchar(50) DEFAULT NULL COMMENT '备注',
  `zdate` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `isdelete` int(11) NOT NULL DEFAULT '0',
  PRIMARY KEY (`id`),
  KEY `uid` (`uid`)
) ENGINE=InnoDB AUTO_INCREMENT=18 DEFAULT CHARSET=utf8mb4;

-- ----------------------------
-- Records of db_wallets_zengjian
-- ----------------------------
INSERT INTO `db_wallets_zengjian` VALUES ('15', '1', 'admin', '管理员', '1', 'mey', '奖金币', '0', '0.00', '100.00', '100.00', '', '2021-07-27 11:37:25', '0');
INSERT INTO `db_wallets_zengjian` VALUES ('16', '1', 'admin', '管理员', '2', 'bdb', '报单币', '0', '0.00', '100.00', '100.00', '', '2021-07-27 11:37:40', '0');
INSERT INTO `db_wallets_zengjian` VALUES ('17', '11', 'aaa', '1', '1', 'mey', '奖金币', '0', '0.00', '1000.00', '1000.00', '', '2021-07-28 16:32:31', '0');

-- ----------------------------
-- Table structure for db_wallets_zhuanhuan
-- ----------------------------
DROP TABLE IF EXISTS `db_wallets_zhuanhuan`;
CREATE TABLE `db_wallets_zhuanhuan` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `uid` int(11) NOT NULL DEFAULT '0',
  `userid` varchar(50) NOT NULL,
  `username` varchar(50) DEFAULT NULL,
  `cid1` int(11) NOT NULL DEFAULT '0' COMMENT '货币id',
  `codename1` varchar(50) NOT NULL COMMENT '货币英文名称',
  `coinname1` varchar(50) NOT NULL COMMENT '货币中文名称',
  `cid2` int(11) NOT NULL DEFAULT '0' COMMENT '货币id',
  `codename2` varchar(50) NOT NULL COMMENT '货币英文名称',
  `coinname2` varchar(50) NOT NULL COMMENT '货币中文名称',
  `jine` decimal(18,2) NOT NULL DEFAULT '0.00',
  `lx` int(11) NOT NULL DEFAULT '0',
  `zdate` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `isdelete` int(11) NOT NULL DEFAULT '0',
  `beizhu` varchar(255) DEFAULT NULL,
  PRIMARY KEY (`id`),
  KEY `uid` (`uid`)
) ENGINE=InnoDB AUTO_INCREMENT=6 DEFAULT CHARSET=utf8mb4;

-- ----------------------------
-- Records of db_wallets_zhuanhuan
-- ----------------------------
INSERT INTO `db_wallets_zhuanhuan` VALUES ('5', '1', 'admin', '管理员', '2', 'bdb', '报单币', '3', 'gwb', '购物币', '50.00', '0', '2021-07-27 11:38:49', '0', '报单币 转 购物币');

-- ----------------------------
-- Table structure for db_wallets_zhuanhuan_select
-- ----------------------------
DROP TABLE IF EXISTS `db_wallets_zhuanhuan_select`;
CREATE TABLE `db_wallets_zhuanhuan_select` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `cid1` int(11) NOT NULL DEFAULT '0' COMMENT '货币id,cid1转换cid2',
  `cid2` int(11) NOT NULL DEFAULT '0' COMMENT '货币id,cid1转换cid2',
  `codename1` varchar(50) NOT NULL COMMENT '货币英文名称1',
  `codename2` varchar(50) NOT NULL COMMENT '货币英文名称2',
  `coinname1` varchar(50) NOT NULL COMMENT '货币中文名称1',
  `coinname2` varchar(50) NOT NULL COMMENT '货币中文名称2',
  `jinemin` decimal(18,2) NOT NULL DEFAULT '1.00' COMMENT '最小转换金额',
  `jinemax` decimal(18,2) NOT NULL DEFAULT '1.00' COMMENT '最大转换金额',
  `jinebs` int(11) NOT NULL DEFAULT '1' COMMENT '转换倍数',
  `bili` decimal(18,2) NOT NULL DEFAULT '100.00' COMMENT '转换比例',
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=7 DEFAULT CHARSET=utf8mb4;

-- ----------------------------
-- Records of db_wallets_zhuanhuan_select
-- ----------------------------
INSERT INTO `db_wallets_zhuanhuan_select` VALUES ('5', '2', '3', 'bdb', 'gwb', '报单币', '购物币', '1.00', '99999.00', '1', '100.00');
INSERT INTO `db_wallets_zhuanhuan_select` VALUES ('6', '1', '3', 'mey', 'gwb', '奖金币', '购物币', '1.00', '99999.00', '1', '100.00');

-- ----------------------------
-- Table structure for db_wallets_zhuanzhang
-- ----------------------------
DROP TABLE IF EXISTS `db_wallets_zhuanzhang`;
CREATE TABLE `db_wallets_zhuanzhang` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `uid` int(11) NOT NULL DEFAULT '0',
  `userid` varchar(50) NOT NULL,
  `username` varchar(50) DEFAULT NULL,
  `cid` int(11) NOT NULL DEFAULT '0' COMMENT '货币id',
  `codename` varchar(50) NOT NULL COMMENT '货币中文名称',
  `coinname` varchar(50) NOT NULL COMMENT '货币中文名称',
  `zqjine` decimal(18,2) NOT NULL DEFAULT '0.00',
  `zhjine` decimal(18,2) NOT NULL DEFAULT '0.00',
  `jine` decimal(18,2) NOT NULL DEFAULT '0.00',
  `lx` int(11) NOT NULL DEFAULT '0',
  `sid` int(11) NOT NULL DEFAULT '0',
  `suserid` varchar(50) NOT NULL,
  `susername` varchar(50) DEFAULT NULL,
  `szqjine` decimal(18,2) NOT NULL DEFAULT '0.00',
  `szhjine` decimal(18,2) NOT NULL DEFAULT '0.00',
  `beizhu` varchar(255) DEFAULT NULL,
  `zdate` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `isdelete` int(11) NOT NULL DEFAULT '0',
  PRIMARY KEY (`id`),
  KEY `uid` (`uid`),
  KEY `sid` (`sid`)
) ENGINE=InnoDB AUTO_INCREMENT=7 DEFAULT CHARSET=utf8mb4;

-- ----------------------------
-- Records of db_wallets_zhuanzhang
-- ----------------------------
INSERT INTO `db_wallets_zhuanzhang` VALUES ('5', '11', 'aaa', '1', '3', 'gwb', '购物币', '8754.67', '8654.67', '100.00', '0', '1', 'admin', '管理员', '1009.60', '1109.60', '', '2021-07-28 17:18:26', '0');
INSERT INTO `db_wallets_zhuanzhang` VALUES ('6', '1', 'admin', '管理员', '3', 'gwb', '购物币', '1109.60', '1009.60', '100.00', '0', '11', 'aaa', '1', '8654.67', '8754.67', '', '2021-07-28 18:09:32', '0');

-- ----------------------------
-- Table structure for db_wallets_zhuanzhang_select
-- ----------------------------
DROP TABLE IF EXISTS `db_wallets_zhuanzhang_select`;
CREATE TABLE `db_wallets_zhuanzhang_select` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `cid` int(11) NOT NULL DEFAULT '0' COMMENT '货币id',
  `codename` varchar(50) NOT NULL COMMENT '货币英文名称',
  `coinname` varchar(50) NOT NULL COMMENT '货币中文名称',
  `jinemin` decimal(18,2) NOT NULL DEFAULT '1.00' COMMENT '最低转账金额',
  `jinemax` decimal(18,2) NOT NULL DEFAULT '10000.00' COMMENT '最高转账金额',
  `jinebs` int(11) NOT NULL DEFAULT '1' COMMENT '转账倍数',
  `shouxu` decimal(18,2) NOT NULL DEFAULT '0.00' COMMENT '手续费',
  `state` int(11) NOT NULL DEFAULT '0' COMMENT '团队限制，0否1是',
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=51 DEFAULT CHARSET=utf8mb4;

-- ----------------------------
-- Records of db_wallets_zhuanzhang_select
-- ----------------------------
INSERT INTO `db_wallets_zhuanzhang_select` VALUES ('48', '3', 'gwb', '购物币', '1.00', '99999.00', '1', '0.00', '0');
INSERT INTO `db_wallets_zhuanzhang_select` VALUES ('49', '2', 'bdb', '报单币', '1.00', '1.00', '1', '0.00', '0');
INSERT INTO `db_wallets_zhuanzhang_select` VALUES ('50', '1', 'mey', '奖金币', '1.00', '1.00', '1', '0.00', '0');
